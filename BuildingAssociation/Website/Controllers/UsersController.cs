using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Website.Helpers;
using Website.Extensions;
using System.Linq;
using Website.ViewModels;
using System.Security.Claims;

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

        [MyAuthorize(Roles = "Admin")]
        // GET api/users
        public HttpResponseMessage Get()
        {
            var items = _userService.GetAll().Select(x => x.ToViewModel());
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        [MyAuthorize(Roles = "Admin")]
        // GET api/users/5
        public HttpResponseMessage Get(long id)
        {
            var item = _userService.Get(id).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        [MyAuthorize(Roles = "Admin")]
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

        [HttpPost]
        [MyAuthorize(Roles = "User")]
        [ActionName("password")]
        public HttpResponseMessage ChangePassword([FromBody]PasswordViewModel item)
        {
            var identity = (ClaimsIdentity)User.Identity;
            //Getting the ID value
            var ID = Convert.ToInt64(identity.Claims.FirstOrDefault(c => c.Type == "loggedUserId").Value);

            var user = _userService.Get(ID);
            user.Password = item.Password;

            try
            {
                _userService.Update(user);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [MyAuthorize(Roles = "Admin")]
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
