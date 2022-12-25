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

    #region SVTP  
    public class SVTPController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISVTPRepository _SVTPRepository;

        public SVTPController(
           IMediator mediator,
           IMapper mapper,
           ISVTPRepository SVTPRepository
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _SVTPRepository = SVTPRepository;

        }
        [HttpGet("GetSinhVienThuePhong")]
        public async Task<object> GetSinhVienThuePhong([FromQuery] GetSVTPDto input)
        {
            try
            {
                var query = (from obj in _SVTPRepository.GetAll()
                             select new SVTPEntity()
                             {
                                 Id = obj.Id,
                                 MaSinhVien = obj.MaSinhVien,
                                 MaPhong = obj.MaPhong,
                                 Ky = obj.Ky,
                                 CreationTime = obj.CreationTime

                             })
                        .WhereIf(input.Id.HasValue, u => u.Id == input.Id);
                if (query != null)
                {
                    var res = query.ToList();
                    var totalRecs = query.Count();
                    return DataResult.ResultSucces(res, "Get SVTP Thanh Cong!", totalRecs);

                }
                else
                {
                    var res = new List<SVTPEntity>();
                    return DataResult.ResultSucces(res, "Get success!", 0);

                }

            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("CreateOrUpdateSVTP")]
        public async Task<object> PostAsync([FromBody] CreateSVTPDto input)
        {
            try
            {
                //Update
                if (input.Id > 0)
                {
                    await _SVTPRepository.UpdateAsync(input);
                    var data = DataResult.ResultSucces(input, "Insert success !");
                    return data;
                }
                else
                {
                    long id = await _SVTPRepository.InsertAndGetIdAsync(input);
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
        [HttpDelete("DeleteSVTP")]
        public async Task<object> DeleteAsync(long id)
        {
            try
            {
                await _SVTPRepository.DeleteAsync(id);
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
