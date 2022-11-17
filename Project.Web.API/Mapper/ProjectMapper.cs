using AutoMapper;
using Project.Domain.Dto;

namespace Project.Web.API.Mapper
{
    public class VoucherMapper : Profile
    {
        public VoucherMapper()
        {
            CreateMap<CreateStudentDto, StudentDto>().ReverseMap();
        }
    }
}
