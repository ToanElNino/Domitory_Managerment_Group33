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

    #region SVDKP  
    public class SVDKPController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISVDKPRepository _SVDKPRepository;

        public SVDKPController(
           IMediator mediator,
           IMapper mapper,
           ISVDKPRepository SVDKPRepository
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _SVDKPRepository = SVDKPRepository;

        }
        [HttpGet("GetSVDKP")]
        public async Task<object> GetSVDKP([FromQuery] GetSVDKPDto input)
        {
            try
            {
                var query = (from obj in _SVDKPRepository.GetAll()
                             select new SVDKPEntity()
                             {
                                 Id = obj.Id,
                                 MaSinhVien = obj.MaSinhVien,
                                 MaPhong = obj.MaPhong, 
                                 ThoiDiemDK = obj.ThoiDiemDK,   
                                 TrangThai  = obj.TrangThai,    
                                 Ky = obj.Ky
                                
                             })
                        .WhereIf(input.Id.HasValue, u => u.Id == input.Id);
                if (query != null)
                {
                    var res = query.ToList();
                    var totalRecs = query.Count();
                    return DataResult.ResultSucces(res, "Get SVDKP Thanh Cong!", totalRecs);

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
        [HttpPost("CreateOrUpdateSVDKP")]
        public async Task<object> PostAsync([FromBody] CreateSVDKPDto input)
        {
            try
            {
                //Update
                if (input.Id > 0)
                {
                    await _SVDKPRepository.UpdateAsync(input);
                    var data = DataResult.ResultSucces(input, "Insert success !");
                    return data;
                }
                else
                {
                    long id = await _SVDKPRepository.InsertAndGetIdAsync(input);
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
        [HttpDelete("DeleteSVDKP")]
        public async Task<object> DeleteAsync(long id)
        {
            try
            {
                await _SVDKPRepository.DeleteAsync(id);
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
