using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;

namespace Tests
{
    public class UserRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly WebApiShopContext _webApiShopContext;
        private readonly UserRepository _userRepository;
        public UserRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _webApiShopContext = databaseFixture.Context;
            _userRepository = new UserRepository(_webApiShopContext);
        }
        [Fact]
        public async Task addUser()
        {
            // Arrange
            var newUser = new User
            {
                Name = "newuser@example.com",
                FirstName = "New",
                LastName = "User",
                Password = "Securepassword11!!"
            };

            // Act
            var result = await _userRepository.AddUser(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser.Name, result.Name);
        }

        [Fact]
        public async Task getUserById()
        {
            // Arrange
            var user = new User
            {
                Name = "existinguser@example.com",
                FirstName = "Existing",
                LastName = "User",
                Password = "Securepassword11!!"
            };

            await _userRepository.AddUser(user);

            // Act
            var result = await _userRepository.GetUserById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
        }

        [Fact]
        public async Task logIn()
        {
            // Arrange
            var user = new User
            {
                Name = "loginuser@example.com",
                FirstName = "Login",
                LastName = "User",
                Password = "Securepassword11!!"
            };

            await _userRepository.AddUser(user);
            var loginUser = new User { Name = "loginuser@example.com", Password = "Securepassword11!!" };

            // Act
            var result = await _userRepository.LogIn(loginUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
        }

        [Fact]
        public async Task LogIn_InvalidCredentials()
        {
            // Arrange
            // Attempt to log in with incorrect credentials

            var loginUser = new User { Name = "wronguser@example.com", Password = "Wrongpassword11!!" };

            // Act
            var result = await _userRepository.LogIn(loginUser);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task getUsers()
        {
            // Arrange
            var user1 = new User
            {
                Name = "user1@example.com",
                FirstName = "User1",
                LastName = "Test",
                Password = "Password123!"
            };

            var user2 = new User
            {
                Name = "user2@example.com",
                FirstName = "User2",
                LastName = "Test",
                Password = "Password123!"
            };

            await _userRepository.AddUser(user1);
            await _userRepository.AddUser(user2);

            // Act
            var result = await _userRepository.GetUsers();

            // Assert
            Assert.NotNull(result);
            //Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetUserById_NotFound()
        {
            // Arrange
            // No user with this ID exists

            // Act
            var result = await _userRepository.GetUserById(999); // Assuming 999 does not exist

            // Assert
            Assert.Null(result);
        }
    }
}
