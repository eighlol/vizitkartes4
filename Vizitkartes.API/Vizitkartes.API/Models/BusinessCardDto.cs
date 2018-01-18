using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vizitkartes.API.Models
{
    public class BusinessCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
    }
}
