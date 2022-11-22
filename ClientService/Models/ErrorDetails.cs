using System.Text.Json;

namespace ClientService.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public String? Message { get; set; }
        public override String? ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
