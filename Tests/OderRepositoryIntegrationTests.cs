using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Tests
{
    public class OderRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly WebApiShopContext _webApiShopContext;
        private readonly OrderRepository _orderRepository;
        public OderRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _webApiShopContext = databaseFixture.Context;
            _orderRepository = new OrderRepository(_webApiShopContext);
        }

        [Fact]
        public async Task addOrder()
        {
            // Arrange
            var category = new Category
            {
                Name = "Electronics"
            };

            var user = new User
            {
                Name = "testuser@example.com",
                FirstName = "Test",
                LastName = "User",
                Password = "password123"
            };

            var product1 = new Product
            {
                Name = "Product 1",
                CategoryId = 1,
                Description = "Description for Product 1",
                Price = 10,
                ImageUrl = "a.png"
            };

            var product2 = new Product
            {
                Name = "Product 2",
                CategoryId = 1,
                Description = "Description for Product 2",
                Price = 15,
                ImageUrl = "a.png"
            };

            await _webApiShopContext.Categories.AddAsync(category);
            await _webApiShopContext.Users.AddAsync(user);
            await _webApiShopContext.Products.AddAsync(product1);
            await _webApiShopContext.Products.AddAsync(product2);
            await _webApiShopContext.SaveChangesAsync();

            var order = new Order
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Sum = 35, // 2 * 10 + 1 * 15
                UserId = 1,
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId =1, Quantity = 2 },
                new OrderItem { ProductId = 2, Quantity = 1 }
            }
            };

            // Act
            var result = await _orderRepository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.Sum, result.Sum);
            Assert.Equal(2, result.OrderItems.Count); // Verify total items
        }

        [Fact]
        public async Task GetById_ReturnsOrder()
        {
            // Arrange
            var category = new Category
            {
                Name = "Books"
            };

            var user = new User
            {
                Name = "testuser2@example.com",
                FirstName = "Test2",
                LastName = "User2",
                Password = "password456"
            };

            var product = new Product
            {
                Name = "Product 3",
                CategoryId = 1,
                Description = "Description for Product 3",
                Price = 20,
                ImageUrl = "a.png"
            };

            await _webApiShopContext.Categories.AddAsync(category);
            await _webApiShopContext.Users.AddAsync(user);
            await _webApiShopContext.Products.AddAsync(product);
            await _webApiShopContext.SaveChangesAsync();

            var order = new Order
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Sum = 20, // 1 * 20
                UserId = 1,
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 1 }
            }
            };

            await _orderRepository.AddOrder(order);

            // Act
            var result = await _orderRepository.GetOrderById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.Sum, result.Sum);
            Assert.Single(result.OrderItems);
        }

        [Fact]
        public async Task GetById_ReturnsNull()
        {
            // Arrange
            // No order with this ID exists

            // Act
            var result = await _orderRepository.GetOrderById(999); // Assuming 999 does not exist

            // Assert
            Assert.Null(result);
        }
    }

}

