using EmailTemplateLibrary.Model;
using ServiceStack.DataAnnotations;

namespace EmailTemplateLibrary.Storage.Postgres
{
    [Alias("TemplateStore")]
    public class TemplateDto : Template
    {
        [PrimaryKey]
        public string Id { get; set; }
    }
}

