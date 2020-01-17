
using Services.Contracts;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Website.Controllers
{
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
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
            var user = _userService.GetByCredentials(email, password);
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, user);
        }
    }
}
