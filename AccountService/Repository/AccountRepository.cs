using AccountService.DbContexts;
using AccountService.Models;
using AccountService.Models.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AccountService.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly String clientBaseUrl;
        private readonly String movementBaseUrl;
        public AccountRepository(ApplicationDbContext db, IMapper mapper, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            clientBaseUrl = _configuration.GetValue<String>("ClientApiUri");
            movementBaseUrl = _configuration.GetValue<String>("MovementApiUri");
        }
        public async Task<AccountDto> CreateAccount(AccountDto accountDto)
        {
            ClientDto client = await GetClient(accountDto.ClientName);

            if(client == null)
            {
                return null;
            }

            Account account = _mapper.Map<Account>(accountDto);
            account.ClientId = client.ClientId;
            _db.Accounts.Add(account);

            await _db.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<ClientDto> GetClient(string clientName)
        {
            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.GetAsync($"{clientBaseUrl}/{clientName}"))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var client = await JsonSerializer.DeserializeAsync<ClientDto>(stream);
                return client;
            }
        }

        public async Task<List<MovementDto>> GetMovements(string accountId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.GetAsync($"{movementBaseUrl}/{accountId}"))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var movements = await JsonSerializer.DeserializeAsync<List<MovementDto>>(stream);
                return movements;
            }
        }

        public async Task<bool> DeleteAccount(string accountId)
        {
            Account? account = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountId.Equals(accountId));
            if (account == null)
                return false;
            account.AccountState = false;
            _db.Accounts.Update(account);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<AccountDto> GetAccountById(string accountId)
        {
            Account? account = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountId.Equals(accountId));
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> UpdateAccount(AccountDto accountDto)
        {
            Account? account = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountId.Equals(accountDto.AccountId));

            if (account == null)
                return null;

            account.AccountType = accountDto.AccountType;
            //account.AccountAmount = accountDto.AccountAmount;
            account.ActualAmount = accountDto.ActualAmount;

            _db.Accounts.Update(account);

            await _db.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);
        }

        public Task<AccountReportDto> GetReport(DateTime beginDate, DateTime endDate, string clientName)
        {
            throw new NotImplementedException();
        }
    }
}
