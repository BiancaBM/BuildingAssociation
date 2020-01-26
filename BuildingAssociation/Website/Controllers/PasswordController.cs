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
    [MyAuthorize(Roles = "User")]
    public class PasswordController : ApiController
    {
        private IUserService _userService;

        public PasswordController(IUserService userService)
        {
            _userService = userService;
        }

        public HttpResponseMessage Post([FromBody]PasswordViewModel item)
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
            catch (Exception e)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
