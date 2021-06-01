using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailTemplateLibrary.Storage;

namespace AspNetCoreWebApplicationExample
{
    public class IndexController : Controller
    {
        public IActionResult Index(TemplateStorage storage)
        {
            var templates = storage.GetTemplates();
            ViewBag.Templates = templates;
            return View();
        }
    }
}
