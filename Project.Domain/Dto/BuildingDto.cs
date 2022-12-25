using Abp.AutoMapper;
using App.Shared.Common;
using Project.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dto
{
    public class BuildingDto : BuildingEntity
    {
        //public IEnumerable<Rate> Rates { get; set; }
        //public IEnumerable<Items> Items { get; set; }
        // custome
    }
    public class GetBuildingDto
    {
        public long? Id { get; set; }
        //public long? BuildingId { get; set; }
    }

    public class CreateBuildingDto : BuildingEntity
    {
    }
}

