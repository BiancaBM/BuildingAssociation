using Repositories.Entities;
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
    public class ProviderBillsController : ApiController
    {
        private IProviderBillService _providerBillService;

        public ProviderBillsController(IProviderBillService service)
        {
            _providerBillService = service;
        }

        // GET api/providerbills
        public HttpResponseMessage Get()
        {
            var items = _providerBillService.GetAll().Select(x => x.ToViewModel());
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/providerbills/5
        public HttpResponseMessage Get(long id)
        {
            var item = _providerBillService.Get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]ProviderBillViewModel item)
        {
            try
            {
                var bill = item.FromViewModel();

                if (bill.UniqueId.HasValue)
                {
                    _providerBillService.Update(bill);
                }
                else
                {
                    _providerBillService.Insert(bill);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/providerbills/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _providerBillService.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, "Bravo patratel");
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Fi atenta!");
            }
        }
    }
}