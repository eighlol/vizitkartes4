using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Vizitkartes.API.Entities
{
    public class Manager : VizitkartesUser
    {
        public Company Company { get; set; }
    }
}