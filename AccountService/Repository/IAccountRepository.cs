using AccountService.Models.Dtos;

namespace AccountService.Repository
{
    public interface IAccountRepository
    {
        Task<AccountDto> CreateAccount(AccountDto accountDto);
        Task<AccountDto> UpdateAccount(AccountDto accountDto);
        Task<bool> DeleteAccount(String accountId);
        Task<AccountDto> GetAccountById(String accountId);
        Task<AccountReportDto> GetReport(DateTime beginDate,DateTime endDate,String clientName);
    }
}
