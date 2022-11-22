namespace AccountService.Models.Dtos
{
    public class AccountReportDto
    {
        public DateTime MovementDate { get; set; }
        public String? ClientName { get; set; }
        public String? ClientId { get; set; }
        public String? ClientType { get; set; }
        public double AccountAmount { get; set;}
        public bool AccountState { get; set;}
        public double MovementAmount { get; set;}
        public double ActualAmount { get; set;}
    }
}
