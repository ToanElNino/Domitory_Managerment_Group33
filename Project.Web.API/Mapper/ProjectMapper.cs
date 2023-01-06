using AutoMapper;
using Project.Domain.Dto;
using Project.Domain.Entity;

namespace Project.Web.API.Mapper
{
    public class VoucherMapper : Profile
    {
        public VoucherMapper()
        {
            CreateMap<CreateStudentDto, StudentDto>().ReverseMap();
            CreateMap<RegisRoomForStudentDto, SVDKPEntity>().ReverseMap();

        }
    }
}
