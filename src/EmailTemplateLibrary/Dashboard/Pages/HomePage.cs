using EmailTemplateLibrary.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailTemplateLibrary.Dashboard.Pages
{
    public class Template 
    {
        public string TemplateKey { get; set; }
        public string TemplateText { get; set; }
        public string Md5Hash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
    partial class HomePage
    {
        public HomePage()
        {            
        }
    }
}
