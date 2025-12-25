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
        [Fact]
        public async Task GetProducts()
        {
            // Arrange
            var category = new Category
            {
                Name = "Electronics"
            };

            var product1 = new Product
            {
                Name = "Product 1",
                CategoryId = 1,
                Description = "Description for Product 1",
                Price = 10,
                ImageUrl = "a.jpg"
            };

            var product2 = new Product
            {
                Name = "Product 2",
                CategoryId = 1,
                Description = "Description for Product 2",
                Price = 15,
                ImageUrl = "a.jpg"
            };

            await _webApiShopContext.Categories.AddAsync(category);
            await _webApiShopContext.Products.AddAsync(product1);
            await _webApiShopContext.Products.AddAsync(product2);
            await _webApiShopContext.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProducts(null, null, null, null, null, null);

            // Assert
            Assert.NotNull(result);
            //Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == product1.Name);
            Assert.Contains(result, p => p.Name == product2.Name);
        }

        [Fact]
        public async Task GetProductById()
        {
            // Arrange
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
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            // Arrange
            // No product with this ID exists

            // Act
            var result = await _productRepository.GetProductById(999); // Assuming 999 does not exist

            // Assert
            Assert.Null(result);
        }

    }
}
