using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Entities.DTO;
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

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetCategories();
            IEnumerable<CategoryDTO> categoryDTOs = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
            return categoryDTOs;
        }
    }
}
