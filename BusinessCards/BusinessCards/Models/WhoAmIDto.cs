using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class WhoAmIDto
    {
        public int? CompanyId { get; set; }
     
        public int? BusinessCardId { get; set; }

        public string EmployeeStatus { get; set; }
    }
}