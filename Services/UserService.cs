using Entities;
using Repositories;
namespace Services
{
    public class UserService
    {
        UserRepository userRepository = new();
        public List<User> GetUsers()
        {
            return userRepository.GetUsers();
        }

        public User GetUserById(int Id)
        {
            return userRepository.GetUserById(Id);
        }

        public User AddUser(User user)
        {
            return userRepository.AddUser(user);
        }

        public User LogIn(User LogInUser)
        {
            return userRepository.LogIn(logInUser);
        }

        public void UpdateUser(int id, User updeteUser)
        {
            userRepository.UpdateUser(id, updeteUser);
        }
    }
}
