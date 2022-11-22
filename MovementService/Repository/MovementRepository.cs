using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovementService.DbContexts;
using MovementService.Models;
using MovementService.Models.Dtos;
using System.Text;
using System.Text.Json;

namespace MovementService.Repository
{
    public class MovementRepository : IMovementRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly String baseUrl;

        public MovementRepository(ApplicationDbContext db, IMapper mapper, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<String>("AccountApiUri");
        }
        public async Task<MovementOutDto> CreateMovement(MovementDto movementDto)
        {
            AccountDto account = await GetAccount(movementDto.AccountId);

            if (account == null)
            {
                return null;
            }

            Movement movement = new Movement();
            movement.MovementDate = DateTime.Now;
            movement.MovementType = movementDto.AccountType;
            movement.MovementAmount = movementDto.AccountAmount;
            double balance = getBalance(movementDto.Movement);
            if(account.ActualAmount == 0 && balance < 0)
                return null;
            movement.MovementBalance = balance;
            movement.AccountId = account.AccountId;
            account.ActualAmount += balance;
            _db.Movements.Add(movement);

            await _db.SaveChangesAsync();
            await UpdateAccount(account);

            return _mapper.Map<MovementOutDto>(movement);
        }

        public async Task<AccountDto> GetAccount(string accountId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.GetAsync($"{baseUrl}/{accountId}"))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var account = await JsonSerializer.DeserializeAsync<AccountDto>(stream);
                return account;
            }
        }

        public async Task<AccountDto> UpdateAccount(AccountDto account)
        {
            var accountJson = new StringContent(JsonSerializer.Serialize(account),Encoding.UTF8,"application/json");

            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.PostAsync($"{baseUrl}",accountJson))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var accountResponse = await JsonSerializer.DeserializeAsync<AccountDto>(stream);
                return accountResponse;
            }
        }

        public double getBalance(string balanceString)
        {
            double sign = 1;
            if (balanceString.Contains("Retiro"))
                sign = -1;
            return double.Parse(balanceString.Trim().Split(' ')[2]) * sign;
        }

        public async Task<AccountDto> GetClient(string accountId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.GetAsync($"{baseUrl}/{accountId}"))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var account = await JsonSerializer.DeserializeAsync<AccountDto>(stream);
                return account;
            }
        }

        public async Task<bool> DeleteMovement(int movementId)
        {
            Movement? movement = await _db.Movements.FirstOrDefaultAsync(c => c.MovementId == movementId);
            if (movement == null)
                return false;
            movement.MovementState = false;
            _db.Movements.Update(movement);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<MovementOutDto> GetMovementById(int movementId)
        {
            Movement? movement = await _db.Movements.FirstOrDefaultAsync(c => c.MovementId == movementId);
            return _mapper.Map<MovementOutDto>(movement);
        }

        public async Task<List<MovementOutDto>> GetMovementList(string accountId)
        {
            List<Movement> movement = await _db.Movements.Where(c => c.AccountId.Equals(accountId)).ToListAsync();
            return _mapper.Map<List<MovementOutDto>>(movement);
        }

        public async Task<MovementOutDto> UpdateMovement(MovementDto movementDto)
        {
            Movement? movement = await _db.Movements.FirstOrDefaultAsync(m => m.MovementId == movementDto.MovementId);
            if (movement == null)
                return null;

            movement.MovementDate = DateTime.Now;
            movement.MovementType = movementDto.AccountType;
            movement.MovementAmount = movementDto.AccountAmount;
            movement.MovementBalance = getBalance(movementDto.Movement);
            _db.Movements.Update(movement);

            await _db.SaveChangesAsync();

            return _mapper.Map<MovementOutDto>(movement);
        }
    }
}
