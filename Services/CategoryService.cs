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

        public async Task<List<CategoryDTO>> GetCategories()
        {
            List<Category> categories = await _categoryRepository.GetCategories();
            List<CategoryDTO> categoryDTOs = _mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return categoryDTOs;
        }
        public async Task<CategoryDTO> AddCategory(CategoryDTO NewCategory)
        {
            if(NewCategory == null)
            {
                throw new ArgumentNullException(nameof(NewCategory));
            }
            Category category = _mapper.Map<CategoryDTO, Category>(NewCategory);
            Category addedCategory = await _categoryRepository.AddCategory(category);
            CategoryDTO addedCategoryDTO = _mapper.Map<Category, CategoryDTO>(addedCategory);
            return addedCategoryDTO;
        }

    }
}
