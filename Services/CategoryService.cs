using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using DTOs;
using Repositories;
namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;

        }
        public bool CheckCategory(CategoryDTO category)
        {
            return category != null;
        }
        public async Task<List<CategoryDTO>> GetCategories()
        {
            List<Category> categories = await _categoryRepository.GetCategories();
            List<CategoryDTO> categoryDTOs = _mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return categoryDTOs;
        }
        public async Task<NewCategoryDTO> GetCategoryById(int id)
        {
            Category? category = await _categoryRepository.GetCategoryById(id);
            NewCategoryDTO categoryDTO = _mapper.Map<Category, NewCategoryDTO>(category);
            return categoryDTO;
        }
        public async Task<NewCategoryDTO> AddCategory(CategoryDTO NewCategory)
        { 
            Category category = _mapper.Map<CategoryDTO, Category>(NewCategory);
            Category addedCategory = await _categoryRepository.AddCategory(category);
            NewCategoryDTO addedCategoryDTO = _mapper.Map<Category, NewCategoryDTO>(addedCategory);
            return addedCategoryDTO;
        }
        public async Task<bool> IsExistsCategoryById(int id)
        {
            return await _categoryRepository.IsExistsCategoryById(id);
        }

    }
}
