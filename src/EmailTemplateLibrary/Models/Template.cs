using System;

namespace EmailTemplateLibrary.Model
{
    /// <summary>
    /// Represents a stored template
    /// </summary>
    public class Template 
    {
        public string TemplateKey { get; set; }
        public string TemplateText { get; set; }
        public string Md5Hash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
