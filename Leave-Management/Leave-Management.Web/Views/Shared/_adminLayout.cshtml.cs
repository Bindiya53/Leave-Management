using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Leave_Management.Web.Views.Shared
{
    public class _adminLayout : PageModel
    {
        private readonly ILogger<_adminLayout> _logger;

        public _adminLayout(ILogger<_adminLayout> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}