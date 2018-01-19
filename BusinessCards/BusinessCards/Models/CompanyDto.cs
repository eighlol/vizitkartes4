using System.Collections.Generic;

namespace Models
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExternalResource { get; set; }
        public IList<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();
        public IList<OfficeDto> Offices { get; set; } = new List<OfficeDto>();
        public BusinessCardDto Manager { get; set; }

    }
}