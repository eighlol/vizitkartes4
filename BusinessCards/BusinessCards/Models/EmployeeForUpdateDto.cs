using System.ComponentModel.DataAnnotations;
using BusinessCards.Entities;

namespace Models
{
    public class EmployeeForUpdateDto
    {
        [Required]
        public int BusinessCardId { get; set; }
        [Required]
        public EmployeeStatus Status { get; set; }
    }
}