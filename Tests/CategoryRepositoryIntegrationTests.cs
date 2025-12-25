using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;


namespace Tests
{
    public class CategoryRepositoryIntegrationTests: IClassFixture<DatabaseFixture> 
    {
        private readonly WebApiShopContext _webApiShopContext;
        private readonly CategoryRepository _categoryRepository;
        public CategoryRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _webApiShopContext= databaseFixture.Context;
            _categoryRepository= new CategoryRepository(_webApiShopContext);
        }
        [Fact]
        public async Task GetCategories_ReturnsCategories()
        {
            // Arrange
            var categoriesToAdd = new List<Category>
            {
                new Category {  Name = "Electronics" },
                new Category {  Name = "Books" }
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
        }

        [Fact]
        public async Task GetCategories_ReturnsEmptyList()
        {
            // Arrange
            // Ensure the database is clean and doesn't contain any categories
            _webApiShopContext.Categories.RemoveRange(_webApiShopContext.Categories);
            await _webApiShopContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
   