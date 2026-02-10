using System.Text.Json;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventDressRentalContext _eventDressRentalContext;
        public UserRepository(EventDressRentalContext eventDressRentalContext)
        {
            _eventDressRentalContext = eventDressRentalContext;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
           return await _eventDressRentalContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
           User? userById = await _eventDressRentalContext.Users.FindAsync(id);
           return userById;
        }

        public async Task<User> AddUser(User user)
        {
            await _eventDressRentalContext.Users.AddAsync(user);
            await _eventDressRentalContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> LogIn(User loginUser)
        {
            User? user = await _eventDressRentalContext.Users.FirstOrDefaultAsync
                (u => (u.FirstName == loginUser.FirstName && u.LastName == loginUser.LastName && u.Password == loginUser.Password));
            return user;
        }

        public async Task<User> UpdateUser(int id, User updateUser)
        {
            User UserToUpdate = await GetUserById(id);
            if (UserToUpdate == null)
            {
                return null;
            }

            _eventDressRentalContext.Entry(UserToUpdate).CurrentValues.SetValues(updateUser);
            await _eventDressRentalContext.SaveChangesAsync();
            return UserToUpdate;
        }

    }
}
