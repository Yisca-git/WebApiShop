using AutoMapper;
using Entities;
using DTOs;

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
        
        public async Task<List<UserDTO>> GetUsers()
        {
            List<User> users = await _userRepository.GetUsers();
            List<UserDTO> userDTOs = _mapper.Map<List<User> , List<UserDTO>>(users);
            return userDTOs;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            User? user = await _userRepository.GetUserById(id);
            if(user == null)
            {
               throw new Exception($"User aith ID {id} not found");
            }
            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> AddUser(UserRegisterDTO newUser)
        {
            if(newUser == null)
            {
                throw new Exception(nameof(newUser));   
            }
            if (_userPasswordService.CheckPassword(newUser.Password) <= 2)
            {
                throw new Exception("Password is too weak.");
            }
            User userRegister = _mapper.Map<UserRegisterDTO, User>(newUser);
            User user = await _userRepository.AddUser(userRegister);
            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> LogIn(UserLoginDTO exsistUser)
        {
            if (exsistUser == null)
            {
                throw new Exception(nameof(exsistUser));
            }
            User LogInUser = _mapper.Map<UserLoginDTO, User>(exsistUser);
            User? user = await _userRepository.LogIn(LogInUser);
            if(user == null)
            {
                throw new Exception("Invalid user name or user password");
            }
            UserDTO userLoginDTO = _mapper.Map<User, UserDTO>(user);
            return userLoginDTO;
        }

        public async Task UpdateUser(int id, UserDTO updateUser)
        {
            if (updateUser == null)
            {
                throw new Exception(nameof(updateUser));
            }
            if (_userPasswordService.CheckPassword(updateUser.Password) <= 2)
            {
                throw new Exception("Password is too weak.");
            }
            User user = _mapper.Map<UserDTO, User>(updateUser);
            await _userRepository.UpdateUser(id, user);
        }
    }
}
