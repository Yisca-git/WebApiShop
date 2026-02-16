using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User user);
        Task<User> LogIn(User loginUser);
        Task<User> UpdateUser(int id, User updateUser);
        Task<bool> IsExistsUserById(int id);
    }
}