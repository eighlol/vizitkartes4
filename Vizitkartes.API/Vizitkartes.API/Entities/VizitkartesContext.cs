using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vizitkartes.API.Entities
{
    public class VizitkartesContext : IdentityDbContext<VizitkartesUser>
    {
        public VizitkartesContext(DbContextOptions<VizitkartesContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<BusinessCard> BusinessCards { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<User> Users { get; set; }
    }
}