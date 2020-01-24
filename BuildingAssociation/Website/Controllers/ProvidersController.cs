using Services.Contracts;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Website.ViewModels;
using Website.Extensions;
using Website.Helpers;

namespace Website.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [BasicAuthentication]
    [MyAuthorize(Roles = "Admin")]
    public class ProvidersController : ApiController
    {
        private IProviderService _providerService;

        public ProvidersController(IProviderService service)
        {
            _providerService = service;
        }

        // GET api/providers
        public HttpResponseMessage Get()
        {
            var items = _providerService.GetAll().Select(x => x.ToViewModel());
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/providers/5
        public HttpResponseMessage Get(long id)
        {
            var item = _providerService.Get(id).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]ProviderViewModel item)
        {
            try
            {
                var entity = item.FromViewModel();

                if (entity.UniqueId.HasValue)
                {
                    _providerService.Update(entity);
                }
                else
                {
                    _providerService.Insert(entity);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/providers/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _providerService.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
