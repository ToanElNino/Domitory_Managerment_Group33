using Abp.AutoMapper;
using App.Shared.Common;
using Project.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dto
{
    public class CanBoDto : CanBoEntity
    {
        //public IEnumerable<Rate> Rates { get; set; }
        //public IEnumerable<Items> Items { get; set; }
        // custome
    }
    public class GetCanBoDto
    {
        public long? Id { get; set; }
        //public long? RoomId { get; set; }
    }

    public class CreateCanBoDto : CanBoEntity
    {
    }
}

