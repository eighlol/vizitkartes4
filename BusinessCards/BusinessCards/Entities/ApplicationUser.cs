using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BusinessCards.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public int? CompanyId { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; } = EmployeeStatus.None;
        public BusinessCard BusinessCard { get; set; }
    }

    public enum EmployeeStatus
    {
        Approved,
        NotApproved,
        Rejected,
        Manager,
        None
    }
}
