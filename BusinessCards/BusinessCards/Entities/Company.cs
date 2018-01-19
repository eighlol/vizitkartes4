using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BusinessCards.Entities
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExternalResource { get; set; }
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Office> Offices { get; set; } = new List<Office>();

        public ApplicationUser Manager
        {
            get { return Employees.FirstOrDefault(user => user.EmployeeStatus == EmployeeStatus.Manager); }
        }

        public ICollection<ApplicationUser> Employees { get; set; } = new List<ApplicationUser>();
    }
}