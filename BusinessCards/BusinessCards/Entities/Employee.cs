using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BusinessCards.Entities
{
    public class Employee : ApplicationUser
    {
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; } = EmployeeStatus.NotApproved;
        public BusinessCard BusinessCard { get; set; }
    }

    public enum EmployeeStatus
    {
        Approved,
        NotApproved,
        Rejected
    }
}