using Application.DTO;
using AutoMapper;
using Domain.Models;

namespace Application.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserGetDTO>();
            CreateMap<UserPostDTO, User>();
            CreateMap<Food, FoodGetDTO>();
            CreateMap<OrderItems, OrderItemGetDTO>();
            CreateMap<OrderPostDTO, Order>();
            CreateMap<OrderItemPostDTO, OrderItems>();
            CreateMap<Order, OrderGetDTO>();
            CreateMap<OrderPatchDTO, Order>();
            CreateMap<OrderItemPatchDTO, OrderItems>();
        }
    }
}
