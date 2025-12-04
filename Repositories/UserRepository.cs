using System.Text.Json;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Shop_WebApiContext _shop_WebApiContext;
        public UserRepository(Shop_WebApiContext shop_WebApiContext)
        {
            _shop_WebApiContext = shop_WebApiContext;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
           return  _shop_WebApiContext.Users;
        }

        public async Task<User> GetUserById(int id)
        {
           User? userById = await _shop_WebApiContext.Users.FindAsync(id);
           return userById;
        }

        public async Task<User> AddUser(User user)
        {
            await _shop_WebApiContext.AddAsync(user);
            await _shop_WebApiContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> LogIn(User loginUser)
        {
            User? user = await _shop_WebApiContext.Users.FirstOrDefaultAsync(u => (u.UserName == loginUser.UserName && u.UserPassword == loginUser.UserPassword));
            return user;
        }

        public async Task<User> UpdateUser(int id, User updateUser)
        {
            User UserToUpdate = await GetUserById(id);
            if (UserToUpdate == null)
            {
                return null;
            }
            
            _shop_WebApiContext.Entry(UserToUpdate).CurrentValues.SetValues(updateUser);
            await _shop_WebApiContext.SaveChangesAsync();
            return UserToUpdate;
        }

    }
}
