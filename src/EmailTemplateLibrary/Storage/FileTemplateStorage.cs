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

        public override void SaveTemplate(string keyName, string content)
        {
            string fileName = $"{keyName}.html";
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }            
            File.WriteAllText(Path.Combine(_folderPath, fileName), content);
        }

        public override void CreateBaseTemplates()
        {
            string registerEmail = EmailTemplateLibraryExtension.RegistrationActivationEmailTemplate();
            SaveTemplate("registerEmail", registerEmail);
        }

    }
}
