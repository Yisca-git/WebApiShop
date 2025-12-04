using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User user);
        Task<User> LogIn(User loginUser);
        Task<User> UpdateUser(int id, User updateUser);
    }
}