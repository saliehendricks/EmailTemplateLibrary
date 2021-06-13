using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailTemplateLibrary.Storage.Sql;
using EmailTemplateLibrary.Storage;

namespace EmailTemplateLibrary.UnitTests
{
    [TestFixture]
    public class SqlContextTest
    {
        [Test]
        public void WhenUsingSqlExtension_ShouldCreateSqlStore()
        {
            //Arrange
            var services = new ServiceCollection();
            services.AddEmailTemplateLibraryServices(new Dashboard.TemplateDashboardOptions()
            {
            })
            .AddSqlServerStorage(new SqlStorageOptions()
            {
                ConnectionString = ":memory:"
            });

            //Act
            var provider = services.BuildServiceProvider();

            //Assert
            var templateStore = provider.GetService<TemplateStorage>();
            Assert.That(templateStore.GetType(), Is.EqualTo(typeof(SqlTemplateStorage)), "SqlTemplateStore not injected");
        }

        [Test]
        public void WhenUsingSqlExtension_ShouldCreateBaseTemplates()
        {
            //Arrange
            var services = new ServiceCollection();
            services.AddEmailTemplateLibraryServices(new Dashboard.TemplateDashboardOptions()
            {
            })
            .AddSqlServerStorage(new SqlStorageOptions()
            {
                ConnectionString = ":memory:"
            });

            var provider = services.BuildServiceProvider();
            var templateStore = provider.GetService<TemplateStorage>();

            templateStore.CreateBaseTemplates();

            //Act
            var templates = templateStore.GetTemplates();

            //Assert
            Assert.That(templates, Is.Not.Null, "No templates created - null returned");
            Assert.That(templates.Count, Is.GreaterThan(0), "No templates created");
        }
    }
}
