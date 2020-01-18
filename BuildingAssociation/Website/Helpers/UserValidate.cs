using Repositories.Entities;
using Repositories.Repositories;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Helpers
{
    public class UserValidate
    {
        public static User GetUserDetails(string email, string password)
        {
            var userRepo = new UserRepository(new Repositories.BuildingAssociationContext());
            var userService = new UserService(userRepo);

            return userService.GetByCredentials(email, password);
        }
    }
}