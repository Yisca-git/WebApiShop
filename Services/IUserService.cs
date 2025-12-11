using Entities;
using Entities.DTO;

namespace Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> GetUserById(int id);

        Task<UserDTO> AddUser(User NewUser);
        Task<UserLoginDTO> LogIn(User loginUser);
        Task<bool> UpdateUser(int id, User updateUser);
    }
}