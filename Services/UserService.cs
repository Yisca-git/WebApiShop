using AutoMapper;
using Entities;
using Entities.DTO;

using Repositories;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordService _userPasswordService;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, IUserPasswordService userPasswordService, IMapper mapper)
        {
            _userRepository = userRepository;
            _userPasswordService = userPasswordService;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            IEnumerable <User> users = await _userRepository.GetUsers();
            IEnumerable<UserDTO> userDTOs = _mapper.Map< IEnumerable<User> ,IEnumerable <UserDTO>>(users);
            return userDTOs;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            User user = await _userRepository.GetUserById(id);
            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> AddUser(User NewUser)
        {
            if (_userPasswordService.CheckPassword(NewUser.UserPassword) <= 2)
            {
                return null;
            }
            User user = await _userRepository.AddUser(NewUser);
            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<UserLoginDTO> LogIn(User loginUser)
        {
            User user = await _userRepository.LogIn(loginUser);
            UserLoginDTO userLoginDTO = _mapper.Map<User, UserLoginDTO>(user);
            return userLoginDTO;
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
