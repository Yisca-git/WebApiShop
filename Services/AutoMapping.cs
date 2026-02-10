using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using DTOs;
namespace Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, NewOrderDTO>().ReverseMap();
            CreateMap<OrderItem, NewOrderItemDTO>().ReverseMap();
            CreateMap<Model, ModelDTO>().ReverseMap();
            CreateMap<Model, NewModelDTO>().ReverseMap();
            CreateMap<Dress, DressDTO>().ReverseMap();
            CreateMap<Dress, NewDressDTO>().ReverseMap();
        }
    }
}
