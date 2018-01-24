using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessCards.Pages
{
    public class CompanyModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "";
        }
    }
}
