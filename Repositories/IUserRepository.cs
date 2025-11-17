using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User GetUserById(int Id);
        List<User> GetUsers();
        User LogIn(User LogInUser);
        void UpdateUser(int id, User updeteUser);
    }
}