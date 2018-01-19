using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BusinessCardForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}