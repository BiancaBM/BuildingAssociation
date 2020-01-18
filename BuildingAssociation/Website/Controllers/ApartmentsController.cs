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
    public class ApartmentsController : ApiController
    {
        private IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService service)
        {
            _apartmentService = service;
        }

        // GET api/apartments
        public HttpResponseMessage Get()
        {
            var items = _apartmentService.GetAll();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/apartments/5
        public HttpResponseMessage Get(long id)
        {
            var item = _apartmentService.Get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]Apartment item)
        {
            try
            {
                if (item.UniqueId.HasValue)
                {
                    _apartmentService.Update(item);
                }
                else
                {
                    _apartmentService.Insert(item);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/apartments/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _apartmentService.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, "Bravo patratel");
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Fi atenta!");
            }
        }
    }
}