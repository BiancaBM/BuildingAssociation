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
    public class MansionsController : ApiController
    {
        private IMansionService _mansionService;

        public MansionsController(IMansionService service)
        {
            _mansionService = service;
        }

        // GET api/mansions
        public HttpResponseMessage Get()
        {
            var items = _mansionService.GetAll().Select(x => x.ToViewModel());
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/mansions/5
        public HttpResponseMessage Get(long id)
        {
            var item = _mansionService.Get(id).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]MansionViewModel item)
        {
            try
            {
                var entity = item.FromViewModel();

                if (entity.UniqueId.HasValue)
                {
                    _mansionService.Update(entity);
                }
                else
                {
                    _mansionService.Insert(entity);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/mansions/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _mansionService.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}