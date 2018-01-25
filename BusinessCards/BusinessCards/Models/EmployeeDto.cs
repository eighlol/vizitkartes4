using System.ComponentModel.DataAnnotations;
using BusinessCards.Entities;

namespace Models
{
    public class EmployeeDto
    {
        public int BusinessCardId { get; set; }
       
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Position { get; set; }

        public string PhoneNumber { get; set; }

        public string EmployeeStatus { get; set; }

    }
}