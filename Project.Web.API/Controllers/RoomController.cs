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

namespace Project.Controllers
{
    [Route("api/services/app/[controller]")]
    [ApiController]

    #region Room  
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRoomRepository _RoomRepository;
        private readonly IMaxUnitOfWork _unitOfWork;

        public RoomController(
           IMediator mediator,
           IMapper mapper,
           IRoomRepository RoomRepository,
           IMaxUnitOfWork unitOfWork
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _RoomRepository = RoomRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpGet("GetRoom")]
        public async Task<object> GetRoom([FromQuery] GetRoomDto input)
        {
            try
            {
                var query = (from obj in _RoomRepository.GetAll()
                             select new RoomEntity()
                             {
                                 Id = obj.Id,                          
                                 MaPhong = obj.MaPhong,
                                 SucChua = obj.SucChua,
                                 SoChotrong = obj.SoChotrong,
                                 GiaPhong = obj.GiaPhong,
                                 ToaId = obj.ToaId,
                                 CreationTime = obj.CreationTime,
                             })
                        .WhereIf(input.Id.HasValue, u => u.Id == input.Id)
                        .WhereIf(input.MaPhong!=null, u => u.MaPhong == input.MaPhong);

                if (query != null)
                {
                    var res = query.ToList();
                    var totalRecs = query.Count();
                    return DataResult.ResultSucces(res, "Get Room Thanh Cong!", totalRecs);

                }
                else
                {
                    var res = new List<RoomEntity>();
                    return DataResult.ResultSucces(res, "Get success!", 0);

                }

            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("CreateOrUpdateRoom")]
        public async Task<object> PostAsync([FromBody] CreateRoomDto input)
        {
            try
            {
                //Update
                if (input.Id > 0)
                {
                    await _RoomRepository.UpdateAsync(input);
                    _unitOfWork.SaveChange();
                    var data = DataResult.ResultSucces(input, "Insert success !");
                    return data;
                }
                else
                {
                    long id = await _RoomRepository.InsertAndGetIdAsync(input);
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
        [HttpDelete("DeleteRoom")]
        public async Task<object> DeleteAsync(long id)
        {
            try
            {
                await _RoomRepository.DeleteAsync(id);
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
