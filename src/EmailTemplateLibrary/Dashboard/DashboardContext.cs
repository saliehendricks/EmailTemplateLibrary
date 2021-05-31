using EmailTemplateLibrary.Storage;
using System.Text.RegularExpressions;

namespace EmailTemplateLibrary.Dashboard
{
    public class DashboardContext 
    {
        public TemplateStorage Storage { get; set; }
        public DashboardOptions Options { get; set; }
        public Match UriMatch { get; set; }
        public DashboardRequest Request { get; set; }
        public DashboardResponse Response { get; set; }
        public string AntiforgeryHeader { get; set; }
        public string AntiforgeryToken { get; set; }
    }
}
