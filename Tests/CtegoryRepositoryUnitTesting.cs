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
    public class CtegoryRepositoryUnitTesting
    {
    
        [Fact]
        public async Task GetCategories_ReturnsCategories()
        {
            // Arrange
            
            var _mockContext = new Mock<WebApiShopContext>();
            var categories = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Books" }
            };
            _mockContext.Setup(ctx => ctx.Categories).ReturnsDbSet(categories);
            var _categoryRepository = new CategoryRepository(_mockContext.Object);

            // Act
            var result = await _categoryRepository.GetCategories();

            // Assert
            Assert.Contains(result, r => r.Name == "Electronics");
            Assert.Contains(result, r => r.Name == "Books");
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCategories_ReturnsEmptyList()
        {
            // Arrange
            var categories = new List<Category>();
            var _mockContext = new Mock<WebApiShopContext>();
            _mockContext.Setup(ctx => ctx.Categories).ReturnsDbSet(categories);
            var _categoryRepository = new CategoryRepository(_mockContext.Object);

            // Act
            var result = await _categoryRepository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}


