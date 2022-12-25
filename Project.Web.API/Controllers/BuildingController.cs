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

    #region Building  
    public class BuildingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IBuildingRepository _BuildingRepository;
        private readonly IMaxUnitOfWork _unitOfWork;

        public BuildingController(
           IMediator mediator,
           IMapper mapper,
           IBuildingRepository BuildingRepository,
           IMaxUnitOfWork unitOfWork
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _BuildingRepository = BuildingRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpGet("GetBuilding")]
        public async Task<object> GetBuilding([FromQuery] GetBuildingDto input)
        {
            try
            {
                var query = (from obj in _BuildingRepository.GetAll()
                             select new BuildingEntity()
                             {
                                 Id = obj.Id,
                                 MaToa = obj.MaToa,
                                 SoTang = obj.SoTang,
                                 SoPhongTrenTang = obj.SoPhongTrenTang,
                                 DiaChi = obj.DiaChi,
                                 MaQuanLy = obj.MaQuanLy,
                                 CreationTime = obj.CreationTime,
                             })
                        .WhereIf(input.Id.HasValue, u => u.Id == input.Id);
                if (query != null)
                {
                    var res = query.ToList();
                    var totalRecs = query.Count();
                    return DataResult.ResultSucces(res, "Get Building Thanh Cong!", totalRecs);

                }
                else
                {
                    var res = new List<BuildingEntity>();
                    return DataResult.ResultSucces(res, "Get success!", 0);

                }

            }
            catch (Exception e)
            {
                var data = DataResult.ResultError(e.ToString(), "Exception !");
                return data;
            }
        }
        [HttpPost("CreateOrUpdateBuilding")]
        public async Task<object> PostAsync([FromBody] CreateBuildingDto input)
        {
            try
            {
                //Update
                if (input.Id > 0)
                {
                    await _BuildingRepository.UpdateAsync(input);
                    _unitOfWork.SaveChange();
                    var data = DataResult.ResultSucces(input, "Insert success !");
                    return data;
                }
                else
                {
                    long id = await _BuildingRepository.InsertAndGetIdAsync(input);
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
        [HttpDelete("DeleteBuilding")]
        public async Task<object> DeleteAsync(long id)
        {
            try
            {
                await _BuildingRepository.DeleteAsync(id);
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
