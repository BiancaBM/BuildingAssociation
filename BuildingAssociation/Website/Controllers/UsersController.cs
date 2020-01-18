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
    public class UsersController : ApiController
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/users
        public HttpResponseMessage Get()
        {
            var items = _userService.GetAll();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/users/5
        public HttpResponseMessage Get(long id)
        {
            var item = _userService.Get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]User item)
        {
            try
            {
                if (item.UniqueId.HasValue)
                {
                    _userService.Update(item);
                }
                else
                {
                    _userService.Insert(item);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/users/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _userService.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, "Bravo patratel");
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Fi atenta!");
            }
        }
    }
}
