using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Entity;
using Project.Domain.Dto;
using Project.Domain.Infastructure;
using System.Data.Entity;
using App.Shared.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.Shared.EntityFrameworkCore.APIResponseBase;
using Nest;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/services/app/[controller]")]
    [ApiController]

    #region Account
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IMaxUnitOfWork _unitOfWork;
        private IConfiguration _config;
        public AccountController(
           IMediator mediator,
           IMapper mapper,
           IAccountRepository accountRepository,
           IMaxUnitOfWork unitOfWork,
           IConfiguration config
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] GetAccountDto input)
        {
            var res = new List<AccountEntity>();
            try
            {
                var query = (from obj in _accountRepository.GetAll()
                             select new AccountEntity()
                             {
                                 Id = obj.Id,
                                 UserName = obj.UserName,  
                                 Password = obj.Password,  
                                 IsActive = obj.IsActive,
                                 RoleID = obj.RoleID,
                                 Role = obj.Role,
                                 UserID = obj.UserID,
                                 CreationTime = obj.CreationTime,
                             })
                        .Where(u => u.UserName == input.UserName && u.Password == input.Password);
                res = query.ToList();
                if(res!= null && res.Count == 1)
                {
                    var account = res[0];
                    var token = GenerateToken(account);
                    var loginResult = new LoginResultDto()
                    {
                        Token = token,
                        UserName = account.UserName,
                        Role = account.Role,
                    };
                    return DataResult.ResultSucces(loginResult, "Dang nhap Thanh Cong!");
                }
                return DataResult.ResultError("user not found", "Dang nhap that bai!");
            }
            catch (Exception ex)
            {
                var data = DataResult.ResultError(ex.ToString(), "Loi dang nhap");
                return data;
            }
        }

        private string GenerateToken(AccountEntity account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.UserName),
                //new Claim(ClaimTypes.Email, account.E),
                //new Claim(ClaimTypes.GivenName, user.GivenName),
                //new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, account.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("Register")]
        public async Task<bool> PostAsync([FromBody] CreateAccountDto input)
        {
            try
            {
                //Update
                if (input.Id > 0)
                {
                    await _accountRepository.UpdateAsync(input);
                    _unitOfWork.SaveChange(input.Id);
                    return true;
                }
                else
                {
                    long id = await _accountRepository.InsertAndGetIdAsync(input);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        [HttpDelete("DeleteAccount")]
        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                await _accountRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }


    #endregion


}
