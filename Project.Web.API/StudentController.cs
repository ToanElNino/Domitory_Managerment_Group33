using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Entity;
using Project.Domain.Dto;
using Project.Domain.Infastructure;
using System.Data.Entity;
using App.Shared.Uow;
using Abp.Collections.Extensions;
using App.Shared.EntityFrameworkCore.APIResponseBase;
using Microsoft.Extensions.Logging;
using Nest;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Web.API
{
    [Route("api/services/app/[controller]")]
    [ApiController]

    #region Student  
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IMaxUnitOfWork _unitOfWork;

        public StudentController(
           IMediator mediator,
           IMapper mapper,
           IStudentRepository studentRepository,
           IMaxUnitOfWork unitOfWork
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpGet("GetStudent")]
        public async Task<object> GetStudent([FromQuery] GetStudentDto input)
        {
            try
            {
                var query = (from obj in _studentRepository.GetAll()
                             select new StudentEntity()
                             {
                                 Id = obj.Id,
                                 MaSinhVien = obj.MaSinhVien,
                                 HoTen = obj.HoTen,
                                 SoDienthoai = obj.SoDienthoai,
                                 GioiTinh = obj.GioiTinh,
                                 NgaySinh = obj.NgaySinh,
                                 Email = obj.Email,
                                 Khoa = obj.Khoa,
                                 Vien = obj.Vien,
                                 Lop = obj.Lop,
                                 HoTenCha = obj.HoTenCha,
                                 HoTenMe = obj.HoTenMe,
                                 NoiThuongTru = obj.NoiThuongTru,
                                 CreationTime = obj.CreationTime
                             })
                        .WhereIf(input.Id.HasValue, u => u.Id == input.Id);
                if (query != null)
                {
                    var res = query.ToList();
                    var totalRecs = query.Count();
                    return DataResult.ResultSucces(res, "Get Student Thanh Cong!", totalRecs);

                }
                else
                {
                    var res = new List<StudentEntity>();
                    return DataResult.ResultSucces(res, "Get success!", 0);

                }

            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("CreateOrUpdateStudent")]
        public async Task<object> PostAsync([FromBody] CreateStudentDto input)
        {
            try
            {
                //Update
                if (input.Id > 0)
                {
                    await _studentRepository.UpdateAsync(input);
                    _unitOfWork.SaveChange();
                    var data = DataResult.ResultSucces(input, "Insert success !");
                    return data;
                }
                else
                {
                    long id = await _studentRepository.InsertAndGetIdAsync(input);
                    _unitOfWork.SaveChange();
                    var data = DataResult.ResultSucces(id, "Insert success !");
                    return data;
                }
            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpDelete("DeleteStudent")]
        public async Task<object> DeleteAsync(long id)
        {
            try
            {
                await _studentRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Delete catch Exception !");
                return data;
            }

        }
    }


    #endregion


}
