using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class Account
    {
        [Key]
        [StringLength(20)]
        public String? AccountId { get; set; }
        [Required]
        [StringLength(20)]
        public String? AccountType { get; set; }
        [Required]
        public double AccountAmount { get; set; }
        [Required]
        public bool AccountState { get; set; }
        [Required]
        public int ClientId { get; set; }
        public double ActualAmount { get; set; }
    }
}
