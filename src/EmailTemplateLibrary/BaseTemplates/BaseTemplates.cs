using System;
using System.Collections.Generic;
using System.Text;

namespace EmailTemplateLibrary.BaseTemplates
{
    public static class BaseTemplates
    {
        public static string RegistrationActivationEmailTemplate()
        {
            string html = $@"<h4>Hi {{firstname}}, Welcome to HAPI</h4><p>Your account is under review and will be activated shortly.</p>
<p>Feel free to familiarize yourself with the documentation and samples in the meant time.</p><br/>";
            return html;
        }
    }
}
