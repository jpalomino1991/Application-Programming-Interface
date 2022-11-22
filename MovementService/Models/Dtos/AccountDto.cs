namespace MovementService.Models.Dtos
{
    public class AccountDto
    {
        public String? AccountId { get; set; }
        public String? AccountType { get; set; }
        public double AccountAmount { get; set; }
        public bool AccountState { get; set; }
        public String? ClientName { get; set; }
        public double ActualAmount { get; set; }
    }
}
