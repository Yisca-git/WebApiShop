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
    public class ProductRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly WebApiShopContext _webApiShopContext;
        private readonly ProductRepository _productRepository;
        public ProductRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _webApiShopContext = databaseFixture.Context;
            _productRepository = new ProductRepository(_webApiShopContext);
        }
        // Setup method to initialize the database state before each test
        private async Task Setup()
        {
            // Clear existing data in the Products and Categories
            _webApiShopContext.Products.RemoveRange(_webApiShopContext.Products);
            _webApiShopContext.Categories.RemoveRange(_webApiShopContext.Categories);
            await _webApiShopContext.SaveChangesAsync();
        }

        // Teardown method to clear the database after each test
        private async Task Teardown()
        {
            // Clear data in the Products and Categories tables
            _webApiShopContext.Products.RemoveRange(_webApiShopContext.Products);
            _webApiShopContext.Categories.RemoveRange(_webApiShopContext.Categories);
            await _webApiShopContext.SaveChangesAsync();
        }

        [Fact]
        public async Task GetProducts()
        {
            // Arrange
            var category = new Category { Name = "Electronics" };
            await _webApiShopContext.Categories.AddAsync(category);
            await _webApiShopContext.SaveChangesAsync();

            var product1 = new Product { Name = "Laptop", Description = "High performance laptop", Price = 1200, CategoryId = 1, ImageUrl = "a.png" };
            var product2 = new Product { Name = "Smartphone", Description = "Latest model smartphone", Price = 800, CategoryId = 1, ImageUrl = "a.png" };
            var product3 = new Product { Name = "Headphones", Description = "Noise cancelling headphones", Price = 100, CategoryId = 1, ImageUrl = "a.png" };

            await _webApiShopContext.Products.AddRangeAsync(product1, product2, product3);
            await _webApiShopContext.SaveChangesAsync();

            // Act
            var (items, totalCount) = await _productRepository.GetProducts("smart", 50, 1000, new int[] { 1 });

            // Assert
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(1, totalCount);
            Assert.Equal("Smartphone", items.First().Name); // Verify the returned product is the smartphone
        }


        [Fact]
        public async Task GetProductById()
        {
            // Arrange
            await Setup();

            var category = new Category
            {
                Name = "Books"
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
            await _webApiShopContext.Products.AddAsync(product);
            await _webApiShopContext.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProductById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);

            // Teardown
            await Teardown();
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            // Arrange
            await Setup();

            // Act
            var result = await _productRepository.GetProductById(999); // Assuming 999 does not exist

            // Assert
            Assert.Null(result);

            // Teardown
            await Teardown();
        }
    }
}