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
using Project.Domain.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/services/app/[controller]")]
    [ApiController]

    #region RegisRoomForStudent  
    public class RegisRoomForStudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly ISVDKPRepository _SVDKPRepository;
        private readonly IMaxUnitOfWork _unitOfWork;

        public RegisRoomForStudentController(
           IMediator mediator,
           IMapper mapper,
           IRoomRepository roomRepository,
          ISVDKPRepository SVDKPRepository,
          IMaxUnitOfWork unitOfWork

            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _roomRepository = roomRepository;
            _SVDKPRepository = SVDKPRepository;
            _unitOfWork = unitOfWork;


        }
        [HttpGet("StudentGetOwnRoomForm")]
        public async Task<object> GetRegisRoomForStudent([FromQuery] long sinhVienID)
        {
            try
            {
                var room = await _SVDKPRepository.FirstOrDefaultAsync(x => x.Id == sinhVienID);
                return DataResult.ResultSucces(room, "Get RegisRoomForStudent Thanh Cong!", 1);

            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("StudentRegisRoom")]
        public async Task<object> PostAsync([FromBody] CreateRegisRoomForm input)
        {
            try
            {
                var room = await _roomRepository.FirstOrDefaultAsync(x => x.MaPhong == input.MaPhong);
                if (room == null || room.SoChotrong <= 0) return DataResult.ResultError("Lỗi", "Không tìm thấy phòng hoặc phòng đã đầy !");
                var dkform = _mapper.Map<SVDKPEntity>(input);
                dkform.Id = (int)CommonENum.STATE_RRERGIS_ROOM_FORM.NEW;
                long id = await _SVDKPRepository.InsertAndGetIdAsync(dkform);
                if (id > 0)
                {
                    room.SoChotrong--;
                    await _roomRepository.UpdateAsync(room);
                    _unitOfWork.SaveChange();
                    return DataResult.ResultSucces(id, "SV đăng ký phòng thành công !");
                }
                else
                {
                    return DataResult.ResultError("Lỗi", "Đăng ký phòng thất bại !");
                }
            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("StudentCancelRegisRoom")]
        public async Task<object> PostCancelAsync([FromBody] CancelRegisRoomForm input)
        {
            try
            {
                var form = await _SVDKPRepository.FirstOrDefaultAsync(x => x.Id == input.formId);
                if (form == null) return DataResult.ResultError("Lỗi", "Không tìm thấy đơn !");
                var room = await _roomRepository.FirstOrDefaultAsync(x => x.MaPhong == form.MaPhong);
                if (room == null) return DataResult.ResultError("Lỗi", "Không tìm thấy phòng !");
                room.SoChotrong++;
                await _roomRepository.UpdateAsync(room);
                form.TrangThai = (int)CommonENum.STATE_RRERGIS_ROOM_FORM.STUDENT_CANCELED;
                await _SVDKPRepository.UpdateAsync(form);
                _unitOfWork.SaveChange();
                return DataResult.ResultSucces(form, "SV hủy đăng ký phòng thành công !");
            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [Obsolete]
        [HttpDelete("UnregisRoomForStudent")]
        public async Task<object> DeleteAsync(long id)
        {
            try
            {
                //await _RegisRoomForStudentRepository.DeleteAsync(id);
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
