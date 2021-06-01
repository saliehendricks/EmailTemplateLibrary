using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailTemplateLibrary.Storage;

namespace AspNetCoreWebApplicationExample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TemplateStorage _templateStore;

        /// <summary>
        /// Dependency for template storage is injected here
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="templateStore"></param>
        public IndexModel(ILogger<IndexModel> logger, TemplateStorage templateStore)
        {
            _logger = logger;
            _templateStore = templateStore;
        }

        public void OnGet()
        {
            //access to templates
            ViewData["Templates"] = _templateStore.GetTemplates();
        }
    }
}
