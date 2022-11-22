using System.ComponentModel.DataAnnotations;

namespace MovementService.Models
{
    public class Movement
    {
        [Key]
        public int MovementId { get; set; }
        [Required]
        public DateTime MovementDate { get; set; }
        [Required]
        [StringLength(20)]
        public String? MovementType { get; set; }
        public bool MovementState { get; set; }
        [Required]
        public double MovementAmount { get; set; }
        [Required]
        public double MovementBalance { get; set; }
        [Required]
        public String? AccountId { get; set; }
    }
}
