using System.Text.Json;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebApiShopContext WebApiShopContext;
        public UserRepository(WebApiShopContext _WebApiShopContext)
        {
            WebApiShopContext = _WebApiShopContext;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
           return await WebApiShopContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
           User? userById = await WebApiShopContext.Users.FindAsync(id);
           return userById;
        }

        public async Task<User> AddUser(User user)
        {
            await WebApiShopContext.Users.AddAsync(user);
            await WebApiShopContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> LogIn(User loginUser)
        {
            User? user = await WebApiShopContext.Users.FirstOrDefaultAsync(u => (u.UserName == loginUser.UserName && u.UserPassword == loginUser.UserPassword));
            return user;
        }

        public async Task<User> UpdateUser(int id, User updateUser)
        {
            User UserToUpdate = await GetUserById(id);
            if (UserToUpdate == null)
            {
                return null;
            }
            
            WebApiShopContext.Entry(UserToUpdate).CurrentValues.SetValues(updateUser);
            await WebApiShopContext.SaveChangesAsync();
            return UserToUpdate;
        }

    }
}
