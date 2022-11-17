using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Entity;
using Project.Domain.Dto;
using Project.Domain.Infastructure;
using System.Data.Entity;
using App.Shared.Uow;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
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
        public async Task<List<StudentEntity>> GetStudent([FromQuery] GetStudentDto input)
        {
            var res = new List<StudentEntity>();
            try
            {
                var query = (from obj in _studentRepository.GetAll()
                             select new StudentEntity()
                             {
                                 Id = obj.Id,
                                 Name = obj.Name,
                                 Address = obj.Address,
                                 StudentCode = obj.StudentCode,
                                 RoomId = obj.RoomId,
                                 CreationTime = obj.CreationTime,
                                 CreatorUserId = obj.CreatorUserId,
                                 LastModificationTime = obj.LastModificationTime,
                                 LastModifierUserId = obj.LastModifierUserId,
                                 IsDeleted = obj.IsDeleted,
                                 DeleterUserId = obj.DeleterUserId,
                                 DeletionTime = obj.DeletionTime,
                             })
                        .Where(u => u.Id == input.Id);
                res = query.ToList();
                return res;
             }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost("CreateOrUpdateStudent")]
        public async Task<bool> PostAsync([FromBody] CreateStudentDto input)
        {
            try
            {
                //Update
                if(input.Id > 0)
                {
                    await _studentRepository.UpdateAsync(input);
                    _unitOfWork.SaveChange(input.Id);
                    return true;
                }
                else
                {
                    long id = await _studentRepository.InsertAndGetIdAsync(input);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        [HttpDelete("DeleteVoucher")]
        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                await _studentRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
              
        }
    }


    #endregion

}
