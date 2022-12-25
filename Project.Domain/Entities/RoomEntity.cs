using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entity
{
    public class RoomEntity : Entity<long>
    {
        [StringLength(20)]
        public string MaPhong { get; set; } 
        public int SucChua { get; set; }
        public int SoChotrong { get; set; }
        public long GiaPhong { get; set; }
        public long ToaId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
