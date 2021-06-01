using System.Reflection;

namespace EmailTemplateLibrary.Dashboard.Pages
{
    partial class LayoutPage 
    {
        public string VersionText
        {
            get
            {
                var version = GetType().GetTypeInfo().Assembly.GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }
    }
}
