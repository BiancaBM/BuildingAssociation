using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Repositories.Contracts;
using Repositories.Entities;
using Repositories.Entities.Enums;
using Services.Contracts;

namespace Services.Services
{
    public class BillGeneratorService : IBillGeneratorService
    {
        private IGeneratedBillRepository _generatedBillRepository;
        private IMansionRepository _mansionRepository;
        private IWaterConsumptionRepository _waterConsumptionRepository;

        private class Item
        {
            public string Key { get; set; }
            public string Title { get; set; }
            public string Value { get; set; }
        }

        private class Line
        {
            public int ApartmentNo { get; set; }
            public IEnumerable<Item> Items { get; set; }
        }

        public BillGeneratorService(
            IMansionRepository mansionRepository,
            IWaterConsumptionRepository waterConsumptionRepository,
            IGeneratedBillRepository generatedBillRepository)
        {
            _mansionRepository = mansionRepository;
            _waterConsumptionRepository = waterConsumptionRepository;
            _generatedBillRepository = generatedBillRepository;
        }

        public void Generate(long mansionId, int month, int year)
        {
            var mansion = _mansionRepository.Get(mansionId);

            var apartments = mansion.Apartments;

            if(!apartments.Any())
            {
                throw new Exception("No apartments on this mansion!");

            }
            var users = mansion.Apartments.Select(x => x.User);
            var numberOfPersonPerMansion = this.GetNumberOfPersonsPerMansion(apartments);
            var mansionBills = mansion.Bills.Where(x => x.CreationDate.Value.Month == month && x.CreationDate.Value.Year == year);

            CheckElectricityBill(mansionBills);
            var waterBill = this.GetWaterBill(mansionBills);

            var billsWithoutWater = mansionBills.Where(x => x.Provider.Type != ProviderType.Water);
            var totalWaterSent = this.GetTotalWaterSent(users, month, year);
            var lostWater = waterBill.Units - totalWaterSent;

            var otherConsumption = mansion.Consumptions.Where(x => x.Date.Value.Month == month && x.Date.Value.Year == year);

            var linesToExport = new List<Line>();

            foreach (var apartment in apartments)
            {
                var line = new Line();
                line.ApartmentNo = apartment.Number;

                var items = new List<Item>();
                items.AddRange(this.GetSplitedBills(billsWithoutWater, numberOfPersonPerMansion, apartment));
                items.Add(this.GetSplitedWaterBill(waterBill, lostWater, numberOfPersonPerMansion, apartment, month, year));
                if(otherConsumption.Any()) items.AddRange(this.GetSplitedConsumptions(otherConsumption, apartment, numberOfPersonPerMansion));

                line.Items = items;

                linesToExport.Add(line);
            }

            var csv = this.GenerateCsv(linesToExport.OrderBy(x => x.ApartmentNo));

            var existingCsvFromDb = _generatedBillRepository.GetAll().FirstOrDefault(x => x.Date.Year == year && x.Date.Month == month && x.MansionId == mansion.UniqueId);
            if (existingCsvFromDb != null)
            {
                existingCsvFromDb.CSV = csv;
                _generatedBillRepository.Update(existingCsvFromDb);
            } else
            {
                var date = new DateTime(year, month, 1);

                _generatedBillRepository.Insert(new GeneratedBill
                {
                    CSV = csv,
                    Date = date,
                    MansionId = mansion.UniqueId
                });
            }
        }

        public GeneratedBill Get(long id)
        {
            return _generatedBillRepository.Get(id);
        }

        public IEnumerable<GeneratedBill> Get(IEnumerable<long> ids)
        {
            return _generatedBillRepository.Get(ids);
        }

        public void Update(GeneratedBill item)
        {
            _generatedBillRepository.Update(item);
        }

        public GeneratedBill Insert(GeneratedBill item)
        {
            return _generatedBillRepository.Insert(item);
        }

        public void Delete(long id)
        {
            _generatedBillRepository.Delete(id);
        }

        public IEnumerable<GeneratedBill> GetAll()
        {
            return _generatedBillRepository.GetAll();
        }

        private string GenerateCsv(IEnumerable<Line> linesToExport)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.Delimiter = ",";

                csvWriter.WriteField("No");

                IEnumerable<string> extractedHeaders = this.ExtractHeaders(linesToExport.FirstOrDefault());

                foreach (var header in extractedHeaders)
                {
                    csvWriter.WriteField(header);
                }
                csvWriter.NextRecord();

                foreach (var line in linesToExport)
                {
                    csvWriter.WriteField(line.ApartmentNo);

                    foreach (var item in line.Items)
                    {
                        csvWriter.WriteField(item.Value);
                    }

                    csvWriter.NextRecord();
                }

                writer.Flush();
                var result = Encoding.UTF8.GetString(mem.ToArray());

                return result;
            }           
        }

        private IEnumerable<string> ExtractHeaders(Line firstLine)
        {
            var result = new List<string>();
            if(firstLine != null)
            {
                foreach(var item in firstLine.Items)
                {
                    result.Add(item.Title);
                }
            }

            return result;
        }

        private IEnumerable<Item> GetSplitedConsumptions(IEnumerable<OtherConsumption> consumptions, Apartment apartment, int numberOfPersonPerMansion)
        {
            var items = new List<Item>();

            foreach(var item in consumptions)
            {
                items.Add(new Item
                {
                    Key = item.Name.Replace(" ", "-"),
                    Title = item.Name,
                    Value = item.CalculationType == CalculationType.IndividualQuota
                    ? (item.Price * (apartment.IndividualQuota / 100)).ToString()
                    : (item.Price / (apartment.MembersCount * numberOfPersonPerMansion)).ToString()
                });
            }

            return items;
        }

        private Item GetSplitedWaterBill(ProviderBill waterBill, double lostWater, int numberOfPersonPerMansion, Apartment apartment, int month, int year)
        {
            double priceForSentWater = GetUserWaterConsumtionSent(apartment.User, month, year);
            double priceForLostWater = lostWater / (numberOfPersonPerMansion * apartment.MembersCount);

            double totalPriceWater = priceForLostWater + priceForSentWater;

            return new Item
            {
                Key = waterBill.Provider.Name.Replace(" ", "-"),
                Title = waterBill.Provider.Name,
                Value = Math.Round(totalPriceWater, 2).ToString()
            };
        }

        private double GetTotalWaterSent(IEnumerable<User> users, int month, int year)
        {
            double totalSent = 0;
            foreach(var user in users)
            {
                double total = GetUserWaterConsumtionSent(user, month, year);
                totalSent += total;
            }

            return totalSent;
        }

        private double GetUserWaterConsumtionSent(User user, int month, int year)
        {
            var thisMonthLastSent = user.WaterConsumptions
                    .Where(x =>
                        x.CreationDate.Value.Month == month
                        && x.CreationDate.Value.Year == year)
                        .OrderByDescending(x => x.CreationDate).FirstOrDefault();

            var precedentMonthLastSent = user.WaterConsumptions
                    .Where(x =>
                        x.CreationDate.Value.Month == (month == 1 ? 12 : month - 1)
                        && x.CreationDate.Value.Year == year)
                        .OrderByDescending(x => x.CreationDate)
                        .FirstOrDefault();

            var lastSent = user.WaterConsumptions
                        .OrderByDescending(x => x.CreationDate)
                        .FirstOrDefault();

            if (thisMonthLastSent != null)
            {
                double total = precedentMonthLastSent != null
                    ? (
                        (thisMonthLastSent.BathroomUnits - precedentMonthLastSent.BathroomUnits) +
                        (thisMonthLastSent.KitchenUnits - precedentMonthLastSent.KitchenUnits)
                       )
                    : thisMonthLastSent.KitchenUnits + thisMonthLastSent.BathroomUnits;

                return total;
            }
            else
            {
                var date = new DateTime(year, month, 28);
                _waterConsumptionRepository.Insert(new WaterConsumption
                {
                    UserId = user.UniqueId,
                    KitchenUnits = lastSent != null ? lastSent.KitchenUnits : 0,
                    BathroomUnits = lastSent != null ? lastSent.BathroomUnits : 0,
                    CreationDate = date
                });
            }

            return 0;
        }

        

        private int GetNumberOfPersonsPerMansion(IEnumerable<Apartment> apartments)
        {
            int numberOfPersonPerMansion = 0;
            foreach (var apartment in apartments)
            {
                numberOfPersonPerMansion += apartment.MembersCount;
            }

            return numberOfPersonPerMansion;
        }

        private void CheckElectricityBill(IEnumerable<ProviderBill> bills)
        {
            var bill = bills.Where(x =>
                    x.Provider.Type == ProviderType.Electricity)
                .OrderByDescending(x => x.CreationDate).FirstOrDefault();

            if (bill == null)
            {
                throw new Exception("No electricity bill!");
            }
        }

        private ProviderBill GetWaterBill(IEnumerable<ProviderBill> bills)
        {
            var bill = bills.Where(x =>
                    x.Provider.Type == ProviderType.Water)
                .OrderByDescending(x => x.CreationDate).FirstOrDefault();

            if (bill == null)
            {
                throw new Exception("No water bill!");
            }

            return bill;
        }

        private IEnumerable<Item> GetSplitedBills(IEnumerable<ProviderBill> otherBills, int numberOfPersonPerMansion, Apartment apartment)
        {
            var items = new List<Item>();
            foreach (var bill in otherBills)
            {
                var billPrice = bill.Units * bill.Provider.UnitPrice + bill.Other;

                //we split bills using 
                items.Add(new Item
                {
                    Key = bill.Provider.Name.Replace(" ", "-"),
                    Title = bill.Provider.Name,
                    Value = Math.Round((billPrice / (numberOfPersonPerMansion * apartment.MembersCount)), 2).ToString()
                });
            }

            return items;
        }
    }
}
