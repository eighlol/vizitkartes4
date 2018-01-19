using System.ComponentModel.DataAnnotations;

namespace Vizitkartes.API.Models
{
    public class BusinessCardForUpdateDto
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Position { get; set; }
        
        public string PhoneNumber { get; set; }
    }
}