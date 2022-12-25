using Abp.AutoMapper;
using App.Shared.Common;
using Project.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dto
{
    public class RoomDto : RoomEntity
    {
        //public IEnumerable<Rate> Rates { get; set; }
        //public IEnumerable<Items> Items { get; set; }
        // custome
    }
    public class GetRoomDto
    {
        public long? Id { get; set; }
        //public long? RoomId { get; set; }
    }

    public class CreateRoomDto : RoomEntity
    {
    }
}

