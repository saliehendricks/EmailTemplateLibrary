using EmailTemplateLibrary.Model;
using System.Collections.Generic;
using System.IO;
namespace EmailTemplateLibrary.Storage
{
    public class FileTemplateStorage : TemplateStorage
    {
        private string _folderPath = "ETLibrary";

        public override string GetTemplate(string keyName)
        {
            string fileName = $"{keyName}.html";
            string result = File.ReadAllText(Path.Combine(_folderPath, fileName));
            return result;
        }

        public override List<Template> GetTemplates(string qry = "")
        {
            List<Template> templates = new List<Template>();
            if (Directory.Exists(_folderPath))
            {
                var files = Directory.GetFiles(_folderPath);
                foreach (var file in files) 
                {
                    FileInfo fi = new FileInfo(file);                    
                    templates.Add(new Template()
                    {
                        TemplateKey = fi.Name,
                        TemplateText = File.ReadAllText(Path.Combine(_folderPath, fi.Name)),
                        Created = fi.CreationTime,
                        Modified = fi.LastWriteTime,
                        Md5Hash = "TODO"                        
                    });
                }
            }
            return templates;
        }

        public override void SaveTemplate(string keyName, string content)
        {
            string fileName = $"{keyName}";
            if (!fileName.EndsWith(".html"))
            {
                fileName = $"{fileName}.html";
            }
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }            
            File.WriteAllText(Path.Combine(_folderPath, fileName), content);
        }

        public override void CreateBaseTemplates()
        {
            string registerEmail = BaseTemplates.BaseTemplates.RegistrationActivationEmailTemplate();
            SaveTemplate("registerEmail", registerEmail);
        }

    }
}
