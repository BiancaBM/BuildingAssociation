using Repositories.Entities;
using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
    [Authorize]
    public class UsersController : BaseController<User>
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
            : base(userService)
        {
            _userService = userService;
        }
    }
}
