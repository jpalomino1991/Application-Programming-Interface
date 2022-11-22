using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public String? ClientPassword { get; set; }
        public bool ClientState { get; set; }
        public int PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
