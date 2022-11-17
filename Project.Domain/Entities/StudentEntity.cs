using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entity
{
    public class StudentEntity : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        [StringLength(8)]
        public string StudentCode { get; set; }
        public string? Address { get; set; }
        public long RoomId { get; set; }
    }
}
