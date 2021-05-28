using EmailTemplateLibrary.Model;
using EmailTemplateLibrary.Storage;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EmailTemplateLibrary.Dashboard.Pages
{
    partial class HomePage
    {
        private List<Template> _templates;
        
        public List<Template> Templates 
        { 
            get
            {
                if (_templates == null) 
                {
                    _templates = Storage.GetTemplates();
                }                
                return _templates;
            }
        }

        public string TemplatesJson 
        {
            get 
            {
                if (_templates == null)
                {
                    _templates = Storage.GetTemplates();
                }
                return JsonSerializer.Serialize(_templates);
            }
        }

    }
}
