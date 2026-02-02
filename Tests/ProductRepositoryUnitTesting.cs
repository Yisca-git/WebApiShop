using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace Tests
{
    public class ProductRepositoryUnitTesting
    {
        [Fact]
        public async Task GetProducts_ReturnsFilteredProducts()
        {
            // Arrange
            var _mockContext = new Mock<WebApiShopContext>();
            var categoryId = 1;
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 1200, CategoryId = categoryId },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 800, CategoryId = categoryId },
                new Product { Id = 3, Name = "Headphones", Description = "Noise cancelling headphones", Price = 100, CategoryId = categoryId }
            };

            _mockContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            var _productRepository = new ProductRepository(_mockContext.Object);
            int[] c = { categoryId };
            // Act
            var (items, totalCount) = await _productRepository.GetProducts("smart", 50, 1000, c, 1, 2);

            // Assert
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(1, totalCount);
            Assert.Equal("Smartphone", items.First().Name); // Verify the returned product is the smartphone
        }


        [Fact]
        public async Task GetProducts_ReturnsEmptyList()
        {
            // Arrange
            var products = new List<Product>();
            var _mockContext = new Mock<WebApiShopContext>();

            _mockContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            var _productRepository = new ProductRepository(_mockContext.Object);

            // Act
            var result = await _productRepository.GetProducts(null, null, null, null, 1, 8);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.items);
        }

        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            // Arrange
            var _mockContext = new Mock<WebApiShopContext>();
            var product = new Product { Id = 1, Name = "Product1", Price = 100, Description = "Description1", CategoryId = 1 };
            var products = new List<Product> { product };

            _mockContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            var _productRepository = new ProductRepository(_mockContext.Object);

            // Act
            var result = await _productRepository.GetProductById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Product1", result.Name);
        }

        [Fact]
        public async Task GetProductById_ReturnsNull()
        {
            // Arrange
            var products = new List<Product>();
            var _mockContext = new Mock<WebApiShopContext>();

            _mockContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            var _productRepository = new ProductRepository(_mockContext.Object);

            // Act
            var result = await _productRepository.GetProductById(999); // Assuming 999 does not exist.

            // Assert
            Assert.Null(result);
        }
    }
}

