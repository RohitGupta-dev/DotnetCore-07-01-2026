using AutoMapper;
using LearningDotnet.Data;
using LearningDotnet.DTO;
using Microsoft.IdentityModel.Tokens;

namespace LearningDotnet.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<Student, StudentDTO>();
            //CreateMap<StudentDTO, Student>();

            CreateMap<StudentDTO, Student>().ReverseMap()
                .ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "no address " : n.Address));
                //.AddTransform<string>(n=> string.IsNullOrEmpty(n)?"No address found":n);
            //CreateMap<StudentDTO, Student>().ReverseMap()
            //    .ForMember(n=>n.Address, opt=>opt.MapFrom(n=>n.Address))
            //    .AddTransform<string>(n=> string.IsNullOrEmpty(n)?"No address found":n);
            //CreateMap<StudentDTO, Student>().ForMember(n=>n.to1,opt=>opt.MapFrom(x=>x.to)).ReverseMap();

        }
    }
}
