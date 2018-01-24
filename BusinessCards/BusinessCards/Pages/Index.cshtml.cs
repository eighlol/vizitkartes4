using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessCards.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAntiforgery _antiForgeryService;

        public IndexModel(IAntiforgery antiForgeryService)
        {
            _antiForgeryService = antiForgeryService;
        }

        public void OnGet()
        {
            var token = _antiForgeryService.GetTokens(HttpContext).RequestToken;
            HttpContext.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false });
        }
    }
}
