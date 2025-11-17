using Entities;

namespace Services
{
    public interface IUserService
    {
        User AddUser(User user);
        User GetUserById(int Id);
        List<User> GetUsers();
        User LogIn(User LogInUser);
        bool UpdateUser(int id, User updeteUser);
    }
}