using Repositories.Contracts;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BuildingAssociationContext _ctx;
        private DbSet<User> Users { get; set; }

        public UserRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            this.Users = context.Users;
        }

        public void Delete(long id)
        {
            var userToBeRemoved = Users.FirstOrDefault(x => x.UniqueId == id);
            Users.Remove(userToBeRemoved);
            
            _ctx.SaveChanges();
        }

        public User Get(long id)
        {
            return Users.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<User> Get(IEnumerable<long> ids)
        {
            return Users.Where(user => ids.Any(id => id == user.UniqueId)).ToList();
        }

        public User Insert(User user)
        {
            var existWithSameEmail = Users.Any(x => x.Email == user.Email);

            if(existWithSameEmail)
            {
                throw new Exception("Exist user with same email!");
            }

            var insertedUser = Users.Add(user);
            _ctx.SaveChanges();

            return insertedUser;
        }

        public void Update(User user)
        {
            var updatedUser = Users.FirstOrDefault(x => x.UniqueId == user.UniqueId);
            updatedUser.Roles = user.Roles;
            updatedUser.Name = user.Name;
            updatedUser.Password = user.Password;
            updatedUser.MansionId = user.MansionId;

            _ctx.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return Users.ToList();
        }

        public User GetByCredentials(string email, string password)
        {
            return Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

    }
}
