using Abp.AutoMapper;
using App.Shared.Common;
using Project.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dto
{
    // Student
    public class RegisRoomForStudentDto
    {
        public long Id { get; set; }
        [StringLength(8)]
        public string MaSinhVien { get; set; }
        [StringLength(20)]
        public string MaPhong { get; set; }

        public DateTime ThoiDiemDK { get; set; }
        public int TrangThai { get; set; }
        [StringLength(10)]
        public string Ky { get; set; }
        public DateTime CreationTime { get; set; }
    }
    public class GetRegisRoomForStudentDto
    {
        public long MaSinhVien { get; set; }
        //public long? RoomId { get; set; }
    }

    public class CreateRegisRoomForm : SVDKPEntity
    {
    }
    public class CancelRegisRoomForm
    {
        public long formId { get; set; }
    }
    //Admin
    public class GetRegisRoomForAdminDto
    {
        public long? Id { get; set; }
        public string? MaSinhVien { get; set; }
        public string? MaPhong { get; set; }
        public int? FormID { get; set; }
        public string? Ky { get; set; }
        public int SortType { get; set; }
        public string? Keyword { get; set; }

        //public DateTime DayFrom { get; set; }
        //public DateTime DayTo { get; set }
        //public long? RoomId { get; set; }
    }
    public class AdminAccpetAFormDto
    {
        public long formId { get; set; }
    }
    public class AdminDenyAFormDto
    {
        public long formId { get; set; }
    }
}

