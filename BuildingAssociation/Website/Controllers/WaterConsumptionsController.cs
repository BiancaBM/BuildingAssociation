using Repositories.Entities;
using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Website.Helpers;

namespace Website.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [BasicAuthentication]
    public class WaterConsumptionsController : ApiController
    {
        private IWaterConsumptionService _waterConsumptionService;

        public WaterConsumptionsController(IWaterConsumptionService service)
        {
            _waterConsumptionService = service;
        }

        // GET api/waterconsumptions
        public HttpResponseMessage Get()
        {
            var items = _waterConsumptionService.GetAll();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/waterconsumptions/5
        public HttpResponseMessage Get(long id)
        {
            var item = _waterConsumptionService.Get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]WaterConsumption item)
        {
            try
            {
                if (item.UniqueId.HasValue)
                {
                    _waterConsumptionService.Update(item);
                }
                else
                {
                    _waterConsumptionService.Insert(item);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/waterconsumptions/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _waterConsumptionService.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, "Bravo patratel");
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Fi atenta!");
            }
        }
    }
}