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
    [MyAuthorize(Roles = "Admin")]
    public class ConsumptionTypeController : ApiController
    {
        private IConsumptionTypeService _service;

        public ConsumptionTypeController(IConsumptionTypeService service)
        {
            _service = service;
        }

        // GET api/consumptiontype
        public HttpResponseMessage Get()
        {
            var items = _service.GetAll().Select(x => x.ToViewModel());
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/consumptiontype/5
        public HttpResponseMessage Get(long id)
        {
            var item = _service.Get(id).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]ConsumptionTypeViewModel item)
        {
            try
            {
                var entity = item.FromViewModel();

                if (entity.UniqueId.HasValue)
                {
                    _service.Update(entity);
                }
                else
                {
                    _service.Insert(entity);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/consumptiontype/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _service.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}