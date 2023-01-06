using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entity
{
    public class SVDKPEntity : Entity<long>
    {
        [StringLength(8)]
        public string MaSinhVien { get; set; }
        [StringLength(20)]
        public string MaPhong { get; set; }

        public DateTime ThoiDiemDK { get; set; }
        public int TrangThai { get; set; }
        [StringLength(10)]
        public string Ky { get; set; }
        public string ThongTinSinhVien { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
