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

    #region CanBo  
    public class CanBoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICanBoRepository _CanBoRepository;
        private readonly IMaxUnitOfWork _unitOfWork;

        public CanBoController(
           IMediator mediator,
           IMapper mapper,
           ICanBoRepository CanBoRepository,
           IMaxUnitOfWork unitOfWork
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _CanBoRepository = CanBoRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpGet("GetCanBo")]
        public async Task<object> GetCanBo([FromQuery] GetCanBoDto input)
        {
            try
            {
                var query = (from obj in _CanBoRepository.GetAll()
                             select new CanBoEntity()
                             {
                                 Id = obj.Id,
                                 GioiTinh = obj.GioiTinh,
                                 NgaySinh = obj.NgaySinh,
                                 Email = obj.Email,
                                 ChucVu = obj.ChucVu,
                                 MaCanBo = obj.MaCanBo,
                                 HoTen = obj.HoTen,
                                 ImageUrl = obj.ImageUrl,
                                 SoDienThoai = obj.SoDienThoai,
                                 CreationTime = obj.CreationTime
                             })
                        .WhereIf(input.Id.HasValue, u => u.Id == input.Id).
                                        WhereIf(input.MaCanBo!=null, u => u.MaCanBo == input.MaCanBo);
                if (query != null)
                {
                    var res = query.ToList();
                    var totalRecs = query.Count();
                    return DataResult.ResultSucces(res, "Get CanBo Thanh Cong!", totalRecs);

                }
                else
                {
                    var res = new List<CanBoEntity>();
                    return DataResult.ResultSucces(res, "Get success!", 0);

                }

            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("CreateOrUpdateCanBo")]
        public async Task<object> PostAsync([FromBody] CreateCanBoDto input)
        {
            try
            {
                //Update
                if (input.Id > 0)
                {
                    await _CanBoRepository.UpdateAsync(input);
                    _unitOfWork.SaveChange();
                    var data = DataResult.ResultSucces(input, "Insert success !");
                    return data;
                }
                else
                {
                    long id = await _CanBoRepository.InsertAndGetIdAsync(input);
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
        [HttpDelete("DeleteCanBo")]
        public async Task<object> DeleteAsync(long id)
        {
            try
            {
                await _CanBoRepository.DeleteAsync(id);
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
