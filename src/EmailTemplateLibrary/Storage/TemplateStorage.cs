using EmailTemplateLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailTemplateLibrary.Storage
{
    public class TemplateStorage
    {
        private static readonly object LockObject = new object();
        private static TemplateStorage _current;

        public static TemplateStorage Current
        {
            get
            {
                lock (LockObject)
                {
                    if (_current == null)
                    {
                        throw new InvalidOperationException("TemplateStorage.Current property value has not been initialized. You must set it before using EmailTemplateLibrary.");
                    }

                    return _current;
                }
            }
            set
            {
                lock (LockObject)
                {
                    _current = value;                    
                }
            }
        }

        public static bool IsBaseTemplatesLoaded { get; set; }

        public virtual string GetTemplate(string keyName) 
        {
            return "Not implemented";
        }

        public virtual void SaveTemplate(string keyName, string content) 
        { }

        public virtual void DeleteTemplate(string keyName)
        { }

        public virtual void CreateBaseTemplates()
        { }

        public virtual List<Template> GetTemplates(string query="")
        {
            throw new NotImplementedException();
        }
    }
}
