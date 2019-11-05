using Repositories.Contracts;
using Repositories.Entities;
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
            var userToBeRemoved = Users.FirstOrDefault(x => x.UserId == id);
            Users.Remove(userToBeRemoved);
            
            _ctx.SaveChanges();
        }

        public User Get(long id)
        {
            return Users.FirstOrDefault(x => x.UserId == id);
        }

        public IEnumerable<User> Get(IEnumerable<long> ids)
        {
            return Users.Where(user => ids.Any(id => id == user.UserId)).ToList();
        }

        public User Insert(User user)
        {
            var insertedUser = Users.Add(user);
            _ctx.SaveChanges();

            return insertedUser;
        }

        public void Update(User user)
        {
            var updatedUser = Users.FirstOrDefault(x => x.UserId == user.UserId);
            updatedUser.IsAdmin = user.IsAdmin;
            updatedUser.MembersCount = user.MembersCount;
            updatedUser.Name = user.Name;
            updatedUser.Password = user.Password;

            _ctx.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return Users.ToList();
        }

    }
}
