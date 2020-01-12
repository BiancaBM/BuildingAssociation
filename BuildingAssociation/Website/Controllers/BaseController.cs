using Repositories.Entities;
using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
    public class BaseController<T> : ApiController where T : BaseEntity
    {
        private IBaseService<T> _service;

        public BaseController(IBaseService<T> service)
        {
            _service = service;
        }

        // GET api/bills
        public HttpResponseMessage Get()
        {
            var items = _service.GetAll();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/bills/5
        public HttpResponseMessage Get(long id)
        {
            var item = _service.Get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]T item)
        {
            try
            {
                if (item.UniqueId.HasValue)
                {
                    _service.Update(item);
                }
                else
                {
                    //item.Guid = Guid.NewGuid();

                    _service.Insert(item);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/bills/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _service.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, "Bravo patratel");
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Fi atenta!");
            }
        }
    }
}