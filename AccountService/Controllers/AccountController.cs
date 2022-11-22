using AccountService.Models.Dtos;
using AccountService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet]
        [Route("{clientName}")]
        public async Task<IActionResult> GetUserById(string accountId)
        {
            AccountDto account = await _accountRepository.GetAccountById(accountId);
            if (account == null)
            {
                return NotFound("No account found with sent parameters");
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto accountDto)
        {
            AccountDto account = await _accountRepository.CreateAccount(accountDto);
            if (account == null)
            {
                return BadRequest("Account already exists");
            }
            return StatusCode(StatusCodes.Status201Created, account);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] AccountDto accountDto)
        {
            AccountDto account = await _accountRepository.UpdateAccount(accountDto);
            if (account == null)
            {
                return BadRequest("Account doesn't exists");
            }
            return Ok(account);
        }

        [HttpDelete]
        [Route("{clientId}")]
        public async Task<IActionResult> DeleteUser(string accountId)
        {
            bool result = await _accountRepository.DeleteAccount(accountId);
            return NoContent();
        }

        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> GetReport(DateTime beginDate,DateTime endDate,string clientName)
        {
            AccountReportDto account = await _accountRepository.GetReport(beginDate,endDate,clientName);
            if (account == null)
            {
                return NotFound("No account found with sent parameters");
            }
            return Ok(account);
        }
    }
}
