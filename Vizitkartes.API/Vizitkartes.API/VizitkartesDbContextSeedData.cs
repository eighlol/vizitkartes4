using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API
{
    public class VizitkartesDbContextSeedData
    {
        private readonly VizitkartesContext _context;
        private readonly UserManager<VizitkartesUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public VizitkartesDbContextSeedData(VizitkartesContext context, UserManager<VizitkartesUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public  async Task EnsureSeedDataForContext()
        {
            if (_context.Companies.Any()) return;

            if (!await _roleManager.RoleExistsAsync(Models.Roles.CompanyManager.ToString()))
            {
                var role = new IdentityRole
                {
                    Name = Models.Roles.CompanyManager.ToString()
                };
                await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync(Models.Roles.Administrator.ToString()))
            {
                var role = new IdentityRole
                {
                    Name = Models.Roles.Administrator.ToString()
                };
                await _roleManager.CreateAsync(role);
            }


            if (!await _roleManager.RoleExistsAsync(Models.Roles.Employee.ToString()))
            {
                var role = new IdentityRole
                {
                    Name = Models.Roles.Employee.ToString()
                };
                await _roleManager.CreateAsync(role);
            }

            if (await _userManager.FindByEmailAsync("manager1@example.com") == null)
            {
                var newUser = new VizitkartesUser
                {
                    UserName = "manager1",
                    Email = "manager1@example.com"
                };
                await _userManager.CreateAsync(newUser, "q1w2e3r4t5");
                await _userManager.AddToRoleAsync(newUser, Models.Roles.CompanyManager.ToString());
            }

            if (await _userManager.FindByEmailAsync("manager2@example.com") == null)
            {
                var newUser = new VizitkartesUser
                {
                    UserName = "manager2",
                    Email = "manager2@example.com"
                };
                await _userManager.CreateAsync(newUser, "q1w2e3r4t5");
                await _userManager.AddToRoleAsync(newUser, Models.Roles.CompanyManager.ToString());
            }
            if (await _userManager.FindByEmailAsync("manager3@example.com") == null)
            {
                var newUser = new VizitkartesUser
                {
                    UserName = "manager3",
                    Email = "manager3@example.com"
                };
                await _userManager.CreateAsync(newUser, "q1w2e3r4t5");
                await _userManager.AddToRoleAsync(newUser, Models.Roles.CompanyManager.ToString());
            }


            if (await _userManager.FindByEmailAsync("employee@example.com") == null)
            {
                var newUser = new VizitkartesUser
                {
                    UserName = "test2",
                    Email = "employee@example.com"
                };
                await _userManager.CreateAsync(newUser, "q1w2e3r4t5");
                await _userManager.AddToRoleAsync(newUser, Models.Roles.Employee.ToString());
            }

            if (await _userManager.FindByEmailAsync("administrator@example.com") == null)
            {
                var newUser = new VizitkartesUser
                {
                    UserName = "test3",
                    Email = "administrator@example.com"
                };
                await _userManager.CreateAsync(newUser, "q1w2e3r4t5");
                await _userManager.AddToRoleAsync(newUser, Models.Roles.Administrator.ToString());
            }
            
            var companies = new List<Company>()
            {
                new Company()
                {
                    Name = "ELKO Grupa",
                    Description =
                        " ELKO Grupa is distributors of IT products and solutions in Europe and Central Asia. The main two business divisions for ELKO are: providing expertise to partners regarding a variety of solutions and services, and wholesale of computer and electronic products.",
                    Departments = new List<Department>()
                    {
                        new Department() {Name = "IT"},
                        new Department() {Name = "Marketing"},
                        new Department() {Name = "Finance"}
                    },
                    ExternalResource = @"http://www.elko.lv/",
                    Offices = new List<Office>()
                    {
                        new Office()
                        {
                            Name = "ELKO Group Headquarters",
                            Address = "Toma iela 4, Riga, Latvia",
                            Fax = "+371 6709 3299"
                        }
                    },
                    Manager = (Manager)await _userManager.FindByEmailAsync("manager1@example.com")
                },
                new Company()
                {
                    Name = "Exigen Services Latvia",
                    Description =
                        "We are a pioneer in IT development in Latvia. Our strength is custom software solutions providing strategically significant, large-scale and integrated IT systems for the national government and for businesses in finance, IT, retail, telecommunication, transport and other sectors.",
                    Departments = new List<Department>()
                    {
                        new Department() {Name = "IT"},
                        new Department() {Name = "Development"},
                        new Department() {Name = "Personal"}
                    },
                    ExternalResource = @"https://exigenservices.lv/",
                    Offices = new List<Office>()
                    {
                        new Office()
                        {
                            Name = "Main office",
                            Address = "J. Dallina iela 15, Riga, Latvia",
                            Fax = "+371 6706 7777"
                        },
                        new Office()
                        {
                            Name = "Regional - Jelgava",
                            Address = "Liela iela 1, Jelgava, Latvia",
                            Fax = "+371 6705 7777"
                        }
                    },
                    Manager = (Manager)await _userManager.FindByEmailAsync("manager2@example.com")
                }
            };
                
            _context.Companies.AddRange(companies);
            _context.SaveChanges();
        }
    }
}