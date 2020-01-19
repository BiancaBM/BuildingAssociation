using Services.Contracts;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Website.Extensions;
using Website.Helpers;
using Website.ViewModels;

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
            var items = _waterConsumptionService.GetAll().Select(x => x.ToViewModel());
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/waterconsumptions/5
        public HttpResponseMessage Get(long id)
        {
            var item = _waterConsumptionService.Get(id).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]WaterConsumptionViewModel item)
        {
            try
            {
                var entity = item.FromViewModel();

                if (entity.UniqueId.HasValue)
                {
                    _waterConsumptionService.Update(entity);
                }
                else
                {
                    _waterConsumptionService.Insert(entity);
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