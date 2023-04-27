using AutoMapper;
using TodoApi.Models.DTO;

namespace TodoApi.Models.AutoMapper
{
    public class CustomAutoMapperProfile : Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<TodoItem, TodoItemDto>();
            base.CreateMap<Student, StudentDto>()
               .ForMember(dest => dest.StudentClassName, sourse => sourse.MapFrom(src => src.StudentClass.Name));
        }
    }
}
