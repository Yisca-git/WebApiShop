using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;


namespace Tests
{
    public class CategoryRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly WebApiShopContext _webApiShopContext;
        private readonly CategoryRepository _categoryRepository;
        public CategoryRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _webApiShopContext = databaseFixture.Context;
            _categoryRepository = new CategoryRepository(_webApiShopContext);
        }
        private async Task Setup()
        {
            // Clear existing data
            _webApiShopContext.Categories.RemoveRange(_webApiShopContext.Categories);
            await _webApiShopContext.SaveChangesAsync();
        }

        // Teardown method to clear the database after each test
        private async Task Teardown()
        {
            // Clear data in the Categories table
            _webApiShopContext.Categories.RemoveRange(_webApiShopContext.Categories);
            await _webApiShopContext.SaveChangesAsync();
        }

        [Fact]
        public async Task GetCategories_ReturnsCategories()
        {
            // Arrange
            await Setup();

            var categoriesToAdd = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Books" }
            };

            await _webApiShopContext.Categories.AddRangeAsync(categoriesToAdd);
            await _webApiShopContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Name == "Electronics");
            Assert.Contains(result, r => r.Name == "Books");

            // Teardown
            await Teardown();
        }

        [Fact]
        public async Task GetCategories_ReturnsEmptyList()
        {
            // Arrange
            await Setup();

            // Act
            var result = await _categoryRepository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            // Teardown
            await Teardown();
        }
    }
}