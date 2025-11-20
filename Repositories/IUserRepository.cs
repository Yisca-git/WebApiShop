using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User GetUserById(int id);
        List<User> GetUsers();
        User LogIn(User loginUser);
        void UpdateUser(int id, User updateUser);
    }
}