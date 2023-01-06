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
    public class AdminRoomRegistionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly ISVDKPRepository _SVDKPRepository;
        private readonly ISVTPRepository _SVTPRepository;
        private readonly IMaxUnitOfWork _unitOfWork;

        public AdminRoomRegistionController(
           IMediator mediator,
           IMapper mapper,
           IRoomRepository roomRepository,
           ISVDKPRepository SVDKPRepository,
           ISVTPRepository SVTPRepository,
           IMaxUnitOfWork unitOfWork

            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _roomRepository = roomRepository;
            _SVDKPRepository = SVDKPRepository;
            _SVTPRepository = SVTPRepository;
            _unitOfWork = unitOfWork;


        }
        [HttpGet("AdminGetForms")]
        public async Task<object> GetRegisRoomForStudent([FromQuery] GetRegisRoomForAdminDto input)
        {
            try
            {

                var query = (from obj in _SVDKPRepository.GetAll()
                             select new SVDKPEntity()
                             {
                                 Id = obj.Id,
                                 CreationTime = obj.CreationTime,
                                 Ky = obj.Ky,
                                 MaPhong = obj.MaPhong,
                                 MaSinhVien = obj.MaSinhVien,
                                 ThoiDiemDK = obj.ThoiDiemDK,
                                 ThongTinSinhVien = obj.ThongTinSinhVien,
                                 TrangThai = obj.TrangThai,
                             })
                       .WhereIf(input.Id.HasValue, u => u.Id == input.Id)
                       .WhereIf(input.MaSinhVien != null, u => u.MaSinhVien == input.MaSinhVien)
                       .WhereIf(input.MaPhong != null, u => u.MaPhong == input.MaPhong)
                       .WhereIf(input.Ky != null, u => u.Ky == input.Ky)
                       .WhereIf(input.Keyword != null, x => (x.MaSinhVien.ToLower().Contains(input.Keyword) || x.Ky.ToLower().Contains(input.Keyword) || x.MaPhong.ToLower().Contains(input.Keyword)));

                if (query != null)
                {
                    var res = query.ToList();
                    var totalRecs = query.Count();
                    return DataResult.ResultSucces(res, "Get don dang ky phong Thanh Cong!", totalRecs);

                }
                else
                {
                    var res = new List<SVDKPEntity>();
                    return DataResult.ResultSucces(res, "Get success!", 0);

                }
            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("AdminAcceptAForm")]
        public async Task<object> PostAsync([FromBody] AdminAccpetAFormDto input)
        {
            try
            {
                var form = await _SVDKPRepository.FirstOrDefaultAsync(x => x.Id == input.formId);
                if (form == null || form.TrangThai != (int)CommonENum.STATE_RRERGIS_ROOM_FORM.NEW) 
                    return DataResult.ResultError("Lỗi", "Không tìm thấy đơn hoặc trạng thái không hợp lệ!");
                var room = await _roomRepository.FirstOrDefaultAsync(x => x.MaPhong == form.MaPhong);
                if (room == null) 
                    return DataResult.ResultError("Lỗi", "Không tìm thấy phòng !");
                form.TrangThai = (int)CommonENum.STATE_RRERGIS_ROOM_FORM.CONFIRMED;
                await _SVDKPRepository.UpdateAsync(form);
                var svtpInsert = new SVTPEntity()
                {
                    Id = 0,
                    MaSinhVien = form.MaSinhVien,
                    MaPhong = form.MaPhong,
                    Ky = form.Ky,
                    CreationTime = new DateTime()
                };
                var svtpId = await _SVTPRepository.InsertAndGetIdAsync(svtpInsert);
                _unitOfWork.SaveChange();
                return DataResult.ResultSucces(form, "Admin xác nhận đơn đăng ký phòng thành công!");
            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("AdminDenyForm")]
        public async Task<object> PostCancelAsync([FromBody] AdminDenyAFormDto input)
        {
            try
            {
                var form = await _SVDKPRepository.FirstOrDefaultAsync(x => x.Id == input.formId);
                if (form == null || form.TrangThai != (int)CommonENum.STATE_RRERGIS_ROOM_FORM.NEW) 
                    return DataResult.ResultError("Lỗi", "Không tìm thấy đơn hoặc trạng thái không hợp lệ!");
                var room = await _roomRepository.FirstOrDefaultAsync(x => x.MaPhong == form.MaPhong);
                if (room == null) 
                    return DataResult.ResultError("Lỗi", "Không tìm thấy phòng !");
                form.TrangThai = (int)CommonENum.STATE_RRERGIS_ROOM_FORM.ADMIN_CANCELED;
                await _SVDKPRepository.UpdateAsync(form);
                _unitOfWork.SaveChange();
                return DataResult.ResultSucces(form, "Admin từ chối đơn đăng ký phòng thành công!");
            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [Obsolete]
        [HttpDelete("abc")]
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
