using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        [StringLength(100)]
        public String? PersonName { get; set; }
        [Required]
        [StringLength(150)]
        public String? PersonDirection { get; set; }
        [StringLength(15)]
        public String? PersonGender { get; set; }
        [Range(0, 99)]
        public int PersonAge { get; set; }
        [StringLength(10)]
        public String? PersonDocumentId { get; set; }
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public String? PersonPhone { get; set; }
    }
}
