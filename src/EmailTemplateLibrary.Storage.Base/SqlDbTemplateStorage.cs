using EmailTemplateLibrary.Model;
using EmailTemplateLibrary.Storage.Base;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailTemplateLibrary.Storage.Base
{
    public class SqlDbTemplateStorage : TemplateStorage
    {
        private readonly OrmLiteConnectionFactory _dbFactory;

        public SqlDbTemplateStorage(string connectionString, IOrmLiteDialectProvider provider)
        {
            _dbFactory = new OrmLiteConnectionFactory(connectionString, provider);
            using (var db = _dbFactory.Open())
            {
                db.CreateTableIfNotExists<TemplateDto>();
            }
        }

        public override string GetTemplate(string keyName)
        {
            string result = "";
            TemplateDto template;
            using (var db = _dbFactory.Open())
            {
                var records = db.Select<TemplateDto>(t => t.TemplateKey == keyName);
                template = records.FirstOrDefault();
            }
            if (template != null)
            {
                result = template.TemplateText;
            }
            return result;
        }

        public override List<Template> GetTemplates(string qryKey = "")
        {
            List<TemplateDto> entities;
            using (var db = _dbFactory.Open())
            {
                var qry = db.From<TemplateDto>();
                if (!string.IsNullOrEmpty(qryKey))
                {
                    qry = qry.Where(t => t.TemplateKey.Contains(qryKey));
                }
                entities = db.Select(qry);
            }

            var templates = entities.ToList().Select(x => x as Template).ToList();
            return templates;
        }

        public override void SaveTemplate(string keyName, string content)
        {
            var t = new TemplateDto()
            {
                TemplateKey = keyName,
                TemplateText = content,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Md5Hash = ""
            };

            TemplateDto template;
            using (var db = _dbFactory.Open())
            {
                template = db.Select<TemplateDto>(x => x.TemplateKey == keyName).FirstOrDefault();

                if (template != null)
                {
                    t.Id = template.Id;
                    db.Update(t);
                }
                else
                {
                    t.Id = Guid.NewGuid().ToString();
                    db.Insert(t);
                }
            }
        }

        public override void DeleteTemplate(string keyName)
        {
            using (var db = _dbFactory.Open())
            {
                db.Delete<TemplateDto>(x => x.TemplateKey == keyName);
            }
        }

        public override void CreateBaseTemplates()
        {
            string registerEmail = BaseTemplates.BaseTemplates.RegistrationActivationEmailTemplate();
            SaveTemplate("registerEmail", registerEmail);
        }
    }
}
