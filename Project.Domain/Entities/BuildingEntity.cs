using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entity
{
    public class BuildingEntity : Entity<long>
    {
        [StringLength(20)]
        public string MaToa { get; set; }
        public int SoTang { get; set; }
        public int? SoPhongTrenTang { get; set; }
        [StringLength(50)]
        public string? DiaChi { get; set; }
        public long? MaQuanLy { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
