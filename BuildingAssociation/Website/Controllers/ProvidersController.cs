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
            var items = _providerService.GetAll();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/providers/5
        public HttpResponseMessage Get(long id)
        {
            var item = _providerService.Get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]Provider item)
        {
            try
            {
                if (item.UniqueId.HasValue)
                {
                    _providerService.Update(item);
                }
                else
                {
                    _providerService.Insert(item);
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
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, "Bravo patratel");
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Fi atenta!");
            }
        }
    }
}
