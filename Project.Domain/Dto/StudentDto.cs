using Abp.AutoMapper;
using App.Shared.Common;
using Project.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dto
{
    public class StudentDto : StudentEntity
    {
        //public IEnumerable<Rate> Rates { get; set; }
        //public IEnumerable<Items> Items { get; set; }
        // custome
    }
    public class GetStudentDto
    {
        public long? Id { get; set; }
        public string? MaSinhVien { get; set; }
        //public long? RoomId { get; set; }
    }

    public class CreateStudentDto : StudentEntity
    {
    }
}

