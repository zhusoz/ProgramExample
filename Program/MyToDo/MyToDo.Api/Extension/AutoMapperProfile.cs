using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dto;

namespace MyToDo.Api.Extension
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ToDoDto, ToDo>().ReverseMap();
            CreateMap<MemoryDto, Memory>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
