using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entity
{
    public class AccountEntity : FullAuditedEntity<long>
    {
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public long RoleID { get; set; }
        public long UserID { get; set; }
        [StringLength(20)]
        public string RoleName { get; set; }
    }
}
