namespace MovementService.Models.Dtos
{
    public class MovementOutDto
    {
        public int MovementId { get; set; }
        public DateTime MovementDate { get; set; }
        public String? MovementType { get; set; }
        public bool MovementState { get; set; }
        public double MovementAmount { get; set; }
        public double MovementBalance { get; set; }
        public String? AccountId { get; set; }
    }
}
