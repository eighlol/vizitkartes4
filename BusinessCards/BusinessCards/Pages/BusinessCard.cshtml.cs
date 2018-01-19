using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace BusinessCards.Pages
{
    public class BusinessCardModel : PageModel
    {
        public string Message { get; set; }
        
        public void OnGet()
        {
            Message = "";
        }
    }
}
