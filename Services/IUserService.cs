using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> LogIn(User loginUser);
        Task<bool> UpdateUser(int id, User updateUser);
    }
}