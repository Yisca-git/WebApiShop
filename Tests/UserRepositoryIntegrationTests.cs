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
        // Setup method to initialize the database state before each test
        private async Task Setup()
        {
            // Clear existing data in the Users table
            _webApiShopContext.Users.RemoveRange(_webApiShopContext.Users);
            await _webApiShopContext.SaveChangesAsync();
        }

        // Teardown method to clear the database after each test
        private async Task Teardown()
        {
            // Clear data in the Users table
            _webApiShopContext.Users.RemoveRange(_webApiShopContext.Users);
            await _webApiShopContext.SaveChangesAsync();
        }

        [Fact]
        public async Task addUser()
        {
            // Arrange
            await Setup();

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

            // Teardown
            await Teardown();
        }

        [Fact]
        public async Task getUserById()
        {
            // Arrange
            await Setup();

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

            // Teardown
            await Teardown();
        }

        [Fact]
        public async Task logIn()
        {
            // Arrange
            await Setup();

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

            // Teardown
            await Teardown();
        }

        [Fact]
        public async Task LogIn_InvalidCredentials()
        {
            // Arrange
            await Setup();

            var loginUser = new User { Name = "wronguser@example.com", Password = "Wrongpassword11!!" };

            // Act
            var result = await _userRepository.LogIn(loginUser);

            // Assert
            Assert.Null(result);

            // Teardown
            await Teardown();
        }

        [Fact]
        public async Task getUsers()
        {
            // Arrange
            await Setup();

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
            // Assert.Equal(2, result.Count); // Uncomment this line if necessary

            // Teardown
            await Teardown();
        }

        [Fact]
        public async Task GetUserById_NotFound()
        {
            // Arrange
            await Setup();

            // Act
            var result = await _userRepository.GetUserById(999); // Assuming 999 does not exist

            // Assert
            Assert.Null(result);

            // Teardown
            await Teardown();
        }
    }
}