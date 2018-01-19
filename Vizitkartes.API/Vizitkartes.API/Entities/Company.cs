using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Vizitkartes.API.Entities
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
        [ForeignKey("ManagerId")]
        public Manager Manager { get; set; }
        public string ManagerId { get; set; }
        public ICollection<VizitkartesUser> Empoloyees { get; set; } = new List<VizitkartesUser>();
    }
}