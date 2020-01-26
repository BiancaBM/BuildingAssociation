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
            var items = _apartmentService.GetAll().Select(x => x.ToViewModel());
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/apartments/5
        public HttpResponseMessage Get(long id)
        {
            var item = _apartmentService.Get(id).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]ApartmentViewModel item)
        {
            try
            {
                var entity = item.FromViewModel();

                if (entity.UniqueId.HasValue)
                {
                    _apartmentService.Update(entity);
                }
                else
                {
                    _apartmentService.Insert(entity);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE api/apartments/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _apartmentService.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}