﻿
using Services.Contracts;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Website.Extensions;

namespace Website.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class AuthenticationController : ApiController
    {
        private IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/authentication
        [HttpGet]
        [ActionName("validate")]
        public HttpResponseMessage ValidateToken(string email, string password)
        {
            var user = _userService.GetByCredentials(email, password).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, user);
        }
    }
}