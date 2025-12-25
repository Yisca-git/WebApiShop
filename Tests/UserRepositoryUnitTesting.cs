using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace Tests
{
    public class UserRepositoryUnitTesting
    {
        [Fact]
        public async Task AddUser()
        {
            // Arrange
            var mockContext = new Mock<WebApiShopContext>();
            var newUser = new User
            {
                Name = "newuser@example.com",
                FirstName = "New",
                LastName = "User",
                Password = "securepassword"
            };
            var users = new List<User>() { newUser };
            mockContext.Setup(m => m.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.AddUser(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser.Name, result.Name);
        }

        [Fact]
        public async Task GetUserById()
        {
            // Arrange
            var mockContext = new Mock<WebApiShopContext>();
            var userRepository = new UserRepository(mockContext.Object);
            var user = new User
            {
                Id = 2,
                Name = "existinguser@example.com",
                FirstName = "Existing",
                LastName = "User",
                Password = "securepassword"
            };

            mockContext.Setup(m => m.Users.FindAsync(user.Id)).ReturnsAsync(user);

            // Act
            var result = await userRepository.GetUserById(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name, result.Name);
        }

        [Fact]
        public async Task LogIn()
        {
            // Arrange
            var mockContext = new Mock<WebApiShopContext>();
            var user = new User
            {
                Name = "loginuser@example.com",
                FirstName = "Login",
                LastName = "User",
                Password = "securepassword"
            };
            var users = new List<User>() { user };
            mockContext.Setup(m => m.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var loginUser = new User { Name = "loginuser@example.com", Password = "securepassword" };

            // Act
            var result = await userRepository.LogIn(loginUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
        }

        [Fact]
        public async Task LogIn_InvalidCredentials()
        {
            // Arrange
            var mockContext = new Mock<WebApiShopContext>();
            var user = new User
            {
                Name = "loginuser@example.com",
                FirstName = "Login",
                LastName = "User",
                Password = "securepassword"
            };
            var users = new List<User>() { user };
            mockContext.Setup(m => m.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var loginUser = new User { Name = "wrong@example.com", Password = "wrongpassword" };

            // Act
            var result = await userRepository.LogIn(loginUser);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task GetUsers()
        {
            // Arrange
            var mockContext = new Mock<WebApiShopContext>();
            var users = new List<User>
          {
          new User { Name = "user1@example.com", FirstName = "User1", LastName = "Test", Password = "password123" },
          new User { Name = "user2@example.com", FirstName = "User2", LastName = "Test", Password = "password123" }
          };

            mockContext.Setup(m => m.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.GetUsers();

            // Assert
            Assert.NotNull(result);
            //Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetUserById_NotFound()
        {
            // Arrange
            var mockContext = new Mock<WebApiShopContext>();
            var userRepository = new UserRepository(mockContext.Object);

            // No user with this ID exists
            var users = new List<User>
         {
         new User { Name = "user1@example.com", FirstName = "User1", LastName = "Test", Password = "password123" },
         new User { Name = "user2@example.com", FirstName = "User2", LastName = "Test", Password = "password123" }
         };

            mockContext.Setup(m => m.Users).ReturnsDbSet(users);

            // Act
            var result = await userRepository.GetUserById(999); // Assuming 999 does not exist

            // Assert
            Assert.Null(result);
        }

    }
}

