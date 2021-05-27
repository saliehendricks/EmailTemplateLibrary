using System.Collections.Generic;
using System.Text;

namespace EmailTemplateLibrary.Dashboard
{

    public class DashboardOptions
    {
        /// <summary>
        /// The path for the Back To Site link. Set to <see langword="null" /> in order to hide the link.
        /// </summary>
        public string AppPath { get; set; }
        public IEnumerable<IDashboardAuthorizationFilter> Authorization { get; set; }
        public bool IgnoreAntiforgeryToken { get; set; }
        public string PrefixPath { get; set; }
    }
}
