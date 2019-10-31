using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Delete(long id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<User> Get(IEnumerable<long> ids)
        {
            return _userRepository.Get(ids);
        }

        public User Get(long id)
        {
            return _userRepository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User Insert(User user)
        {
            return _userRepository.Insert(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }
    }
}
