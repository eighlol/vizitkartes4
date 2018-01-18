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
        public VizitkartesDbContextSeedData(VizitkartesContext context, UserManager<VizitkartesUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public  async Task EnsureSeedDataForContext()
        {
            if (_context.Companies.Any()) return;

            if (await _userManager.FindByEmailAsync("test1@gmail.com") == null)
            {
                var newUser = new VizitkartesUser
                {
                    UserName = "test2",
                    Email = "test1@gmail.com"
                };
                await _userManager.CreateAsync(newUser, "q1w2e3r4t5");
            }

            if (await _userManager.FindByEmailAsync("test2@gmail.com") == null)
            {
                var newUser = new VizitkartesUser
                {
                    UserName = "test2",
                    Email = "test2@gmail.com"
                };
                await _userManager.CreateAsync(newUser, "q1w2e3r4t5");
            }

            if (await _userManager.FindByEmailAsync("test3@gmail.com") == null)
            {
                var newUser = new VizitkartesUser
                {
                    UserName = "test3",
                    Email = "test3@gmail.com"
                };
                await _userManager.CreateAsync(newUser, "q1w2e3r4t5");
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
                    Manager = await _userManager.FindByEmailAsync("test3@gmail.com")
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
                    Manager = await _userManager.FindByEmailAsync("test2@gmail.com")
                }
            };
                
            _context.Companies.AddRange(companies);
            _context.SaveChanges();
        }
    }
}