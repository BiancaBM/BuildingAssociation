using Repositories.Entities;
using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
    public class WaterConsumptionsController : BaseController<WaterConsumption>
    {
        private IWaterConsumptionService _waterConsumptionService;

        public WaterConsumptionsController(IWaterConsumptionService waterConsumptionService)
            : base(waterConsumptionService)
        {
            _waterConsumptionService = waterConsumptionService;
        }
    }
}