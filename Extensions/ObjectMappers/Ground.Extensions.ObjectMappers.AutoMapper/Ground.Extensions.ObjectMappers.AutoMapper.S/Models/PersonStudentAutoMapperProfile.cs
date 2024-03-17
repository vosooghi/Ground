using AutoMapper;

namespace Ground.Extensions.ObjectMappers.AutoMapper.S.Models
{
    public class PersonStudentAutoMapperProfile : Profile
    {
        public PersonStudentAutoMapperProfile()
        {
            CreateMap<Person, Student>().ReverseMap();
        }
    }
}
