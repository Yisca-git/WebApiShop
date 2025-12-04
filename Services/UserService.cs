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
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> AddUser(User user)
        {
            if (_userPasswordService.CheckPassword(user.UserPassword) <= 2)
            {
                return null;
            }
            return await _userRepository.AddUser(user);
        }

        public async Task<User> LogIn(User loginUser)
        {
            return await _userRepository.LogIn(loginUser);
        }

        public async Task<bool> UpdateUser(int id, User updateUser)
        {
            if (_userPasswordService.CheckPassword(updateUser.UserPassword) <= 2)
            {
                return false;
            }
            await _userRepository.UpdateUser(id, updateUser);
            return true;
        }
    }
}
