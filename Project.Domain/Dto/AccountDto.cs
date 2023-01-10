using Abp.AutoMapper;
using App.Shared.Common;
using Project.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dto
{
    public class AccountDto : AccountEntity
    {
        //public IEnumerable<Rate> Rates { get; set; }
        //public IEnumerable<Items> Items { get; set; }
        // custome
    }
    public class GetAccountDto
    {
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
    }

    public class CreateAccountDto : AccountEntity
    {

    }
    public class LoginResultDto
    {
        public string? Token { get; set; }   
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}

