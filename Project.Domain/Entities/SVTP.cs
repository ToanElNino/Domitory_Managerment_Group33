using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entity
{
    public class SVTPEntity : Entity<long>
    {
        [StringLength(8)]
        public string MaSinhVien { get; set; }
        [StringLength(20)]
        public string MaPhong { get; set; }
        [StringLength(10)]
        public string Ky { get; set; }
        public DateTime CreationTime { get; set; }  
    }
}
