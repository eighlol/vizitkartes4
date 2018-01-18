using System.Collections.Generic;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API.Models
{
    public class OfficeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
    }
}