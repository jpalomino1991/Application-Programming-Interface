namespace AccountService.Models.Dtos
{
    public class ClientDto
    {
        public String? PersonName { get; set; }
        public String? PersonDirection { get; set; }
        public String? PersonPhone { get; set; }
        public String? ClientPassword { get; set; }
        public bool ClientState { get; set; }
        public int ClientId { get; set; }
    }
}
