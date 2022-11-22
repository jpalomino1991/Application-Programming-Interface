namespace MovementService.Models.Dtos
{
    public class MovementDto
    {
        public String? AccountId { get; set; }
        public String? AccountType { get; set; }
        public double AccountAmount { get; set; }
        public bool AccountState { get; set; }
        public String? Movement { get; set; }
        public int MovementId { get; set; }
    }
}
