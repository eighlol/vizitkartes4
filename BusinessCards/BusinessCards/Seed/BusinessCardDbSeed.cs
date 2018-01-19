using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCards.Controllers.Api;
using BusinessCards.Entities;
using BusinessCards.Entities.Migrations;
using BusinessCards.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BusinessCards.Seed
{
    public class BusinessCardDbSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BusinessCardDbSeed(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task EnsureSeed()
        {
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

            if (await _userManager.FindByEmailAsync("admin@example.com") == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com"
                };
                await _userManager.CreateAsync(newUser, "Qwerty1.");
                await _userManager.AddToRoleAsync(newUser, Models.Roles.Administrator.ToString());
            }

            if (await _userManager.FindByEmailAsync("manager1@example.com") == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "manager1@example.com",
                    Email = "manager1@example.com"
                };
                await _userManager.CreateAsync(newUser, "Qwerty1.");
                await _userManager.AddToRoleAsync(newUser, Models.Roles.CompanyManager.ToString());
            }

            if (await _userManager.FindByEmailAsync("manager2@example.com") == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "manager2@example.com",
                    Email = "manager2@example.com"
                };
                await _userManager.CreateAsync(newUser, "Qwerty1.");
                await _userManager.AddToRoleAsync(newUser, Models.Roles.CompanyManager.ToString());
            }

            if (!_context.Companies.Any())
            {
                var manager1 = await _userManager.FindByEmailAsync("manager1@example.com");
                manager1.EmployeeStatus = EmployeeStatus.Manager;
                var manager2 = await _userManager.FindByEmailAsync("manager2@example.com");
                manager2.EmployeeStatus = EmployeeStatus.Manager;
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
                        Employees = new List<ApplicationUser>()
                        {
                            manager1
                        }

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
                        //Manager = new Manager(){Id = manager2.Id},
                        Employees = new List<ApplicationUser>()
                        {
                            manager2
                        },
                    }
                };
                //var manager123 = new Manager() { Company = }
                
                _context.Companies.AddRange(companies);
                _context.SaveChanges();
            }
        }

    }
}
