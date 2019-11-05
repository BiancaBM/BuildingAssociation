using Repositories.Entities;
using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
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
            var users = _userService.GetAll();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, users);
        }

        // GET api/users/5
        public HttpResponseMessage Get(long id)
        {
            var user = _userService.Get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, user);
        }

        // POST api/users
        public HttpResponseMessage Post([FromBody]User user)
        {
            try
            {
                if(user.UserId.HasValue)
                {
                    _userService.Update(user);
                } else
                {
                    user.DateCreated = DateTime.UtcNow;
                    user.Guid = Guid.NewGuid();

                    _userService.Insert(user);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            } catch (Exception)
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
