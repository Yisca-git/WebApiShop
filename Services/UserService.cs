using Entities;
using Microsoft.VisualBasic;
using Repositories;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordService _userPasswordService;
        public UserService(IUserRepository userRepository, IUserPasswordService userPasswordService)
        {
            _userRepository= userRepository;
            _userPasswordService= userPasswordService;
        }
        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetUserById(int Id)
        {
            return _userRepository.GetUserById(Id);
        }

        public User AddUser(User user)
        {
            if(_userPasswordService.CheckPassword(user.Password)<=2)
            {
                return null;
            }
            return _userRepository.AddUser(user);
        }

        public User LogIn(User LogInUser)
        {
            
            return _userRepository.LogIn(LogInUser);
        }

        public bool UpdateUser(int id, User updeteUser)
        {
            if (_userPasswordService.CheckPassword(updeteUser.Password) <= 2)
            {
                return false;
            }
            _userRepository.UpdateUser(id, updeteUser);
            return true;
        }
    }
}
