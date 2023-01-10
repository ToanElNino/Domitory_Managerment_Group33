using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entity
{
    public class CanBoEntity : Entity<long>
    {
        [StringLength(50)]
        public string HoTen { get; set; }
        public string MaCanBo { get; set; }
        [StringLength(50)]
        public string? ChucVu { get; set; }
        [StringLength(10)]
        public string? GioiTinh { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }
        [StringLength(11)]
        public string? SoDienThoai { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
