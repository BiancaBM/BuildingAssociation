using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Website.Helpers;
using Website.Extensions;
using System.Linq;
using Website.ViewModels;

namespace Website.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [BasicAuthentication]
    [MyAuthorize(Roles = "Admin")]
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
            var items = _userService.GetAll().Select(x => x.ToViewModel());
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/users/5
        public HttpResponseMessage Get(long id)
        {
            var item = _userService.Get(id).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        public HttpResponseMessage Post([FromBody]UserViewModel item)
        {
            try
            {
                var userEntity = item.FromViewModel();
                userEntity.Roles = "User";

                if (userEntity.UniqueId.HasValue)
                {
                    _userService.Update(userEntity);
                }
                else
                {
                    _userService.Insert(userEntity);
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
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
