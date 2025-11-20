using Entities;

namespace Services
{
    public interface IUserService
    {
        User AddUser(User user);
        User GetUserById(int id);
        List<User> GetUsers();
        User LogIn(User loginUser);
        bool UpdateUser(int id, User updateUser);
    }
}