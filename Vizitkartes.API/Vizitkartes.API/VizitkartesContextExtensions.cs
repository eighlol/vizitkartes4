using System.Collections.Generic;
using System.Linq;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API
{
    public static class VizitkartesContextExtensions
    {
        public static void EnsureSeedDataForContext(this VizitkartesContext context)
        {
            if (context.Companies.Any()) return;
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Registred"
                },
                new Role()
                {
                    Name = "Administrator"
                }
            };
            var users = new List<User>()
            {
                new User() {Role = roles[0]},
                new User() {Role = roles[0]},
                new User() {Role = roles[0]},
                new User() {Role = roles[1]}
            };
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
                    Manager = users[0]
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
                    Manager = users[1]
                }
            };
                
            context.Users.AddRange(users);
            context.Companies.AddRange(companies);
            context.SaveChanges();
        }
    }
}