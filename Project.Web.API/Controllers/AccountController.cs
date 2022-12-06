using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Entity;
using Project.Domain.Dto;
using Project.Domain.Infastructure;
using System.Data.Entity;
using App.Shared.Uow;
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

        public AccountController(
           IMediator mediator,
           IMapper mapper,
           IAccountRepository accountRepository,
           IMaxUnitOfWork unitOfWork
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpGet("Login")]
        public async Task<List<AccountEntity>> GetAccount([FromQuery] GetAccountDto input)
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
                                 RoleName = obj.RoleName,
                                 UserID = obj.UserID,
                                 CreationTime = obj.CreationTime,
                                 CreatorUserId = obj.CreatorUserId,
                                 LastModificationTime = obj.LastModificationTime,
                                 LastModifierUserId = obj.LastModifierUserId,
                                 IsDeleted = obj.IsDeleted,
                                 DeleterUserId = obj.DeleterUserId,
                                 DeletionTime = obj.DeletionTime,
                             })
                        .Where(u => u.UserName == input.UserName && u.Password == input.Password);
                res = query.ToList();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
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
