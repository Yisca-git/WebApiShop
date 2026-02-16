using Entities;
using DTOs;

namespace Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetUsers();
        Task<UserDTO> GetUserById(int id);

        Task<UserDTO> AddUser(UserRegisterDTO NewUser);
        Task<UserDTO> LogIn(UserLoginDTO exsistUser);
        Task UpdateUser(int id, UserDTO updateUser);
        Task<bool> IsExistsUserById(int id);

    }
}