using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Vizitkartes.API.Entities
{
    public class VizitkartesUser : IdentityUser
    {
        public BusinessCard BusinessCard { get; set; }
    }
}
