using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entity
{
    public class StudentEntity : Entity<long>
    {
        [StringLength(50)]
        public string HoTen { get; set; }
        [StringLength(8)]
        public string MaSinhVien { get; set; }
        [StringLength(11)]

        public string SoDienthoai { get; set; }
        [StringLength(10)]

        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        [StringLength(30)]

        public string Email { get; set; }
        [StringLength(30)]

        public string Khoa { get; set; }
        [StringLength(30)]

        public string Vien { get; set; }
        [StringLength(30)]

        public string Lop { get; set; }
        [StringLength(50)]

        public string? HoTenCha { get; set; }
        [StringLength(50)]

        public string? HoTenMe { get; set; }
        [StringLength(100)]

        public string? NoiThuongTru { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
