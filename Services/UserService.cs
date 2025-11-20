using Entities;
using Repositories;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordService _userPasswordService;
        
        public UserService(IUserRepository userRepository, IUserPasswordService userPasswordService)
        {
            _userRepository = userRepository;
            _userPasswordService = userPasswordService;
        }
        
        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public User AddUser(User user)
        {
            if(_userPasswordService.CheckPassword(user.Password) <= 2)
            {
                return null;
            }
            return _userRepository.AddUser(user);
        }

        public User LogIn(User loginUser)
        {
            return _userRepository.LogIn(loginUser);
        }

        public bool UpdateUser(int id, User updateUser)
        {
            if (_userPasswordService.CheckPassword(updateUser.Password) <= 2)
            {
                return false;
            }
            _userRepository.UpdateUser(id, updateUser);
            return true;
        }
    }
}
