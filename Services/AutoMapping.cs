using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Entities.DTO;
namespace Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, UserLoginDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName,
                   opt => opt.MapFrom(src => src.Category.CategoryName));

        }
    }
}
