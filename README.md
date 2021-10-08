# Email Template Library

[![Nuget](https://github.com/saliehendricks/EmailTemplateLibrary/actions/workflows/publish.yml/badge.svg)](https://github.com/saliehendricks/EmailTemplateLibrary/actions/workflows/publish.yml)

## Overview
An easy way to add string based templates to your dotnet application. Store and modify string or html based templates for emails, sms' or anything else that requires a template.

![image](https://user-images.githubusercontent.com/1830594/120047159-35d59200-c014-11eb-9448-88244a623027.png)

Storage options:
- File: Stores templates on disk. Suitable for single site web applications or service.
- Mongo: Stores templates in MongoDB
- SQL : Stores templates in a standard SQL table
- Postgres : as above

# Base Templates
Optionally include base templates:
- NewUserActivationEmail
- NewUserAdminActivationEmail - TODO
- PasswordResetEmail - TODO

# Installation

```
PM> Install-Package EmailTemplateLibrary
```
Then update your Startup.cs file:

```cs
services.AddEmailTemplateLibraryServices(new TemplateDashboardOptions()
            {
                IgnoreAntiforgeryToken = true,
                LoadBaseTemplates = true
            });
```
and
```cs         
public void Configuration(IAppBuilder app)
{
    app.UseEmailTemplateLibrary(); // Defaults to file system
}
```

## Storage Options

Mongo:

Add a reference to EmailTemplateLibrary.Storage.Mongo and in your Startup.cs:
```cs
using EmailTemplateLibrary.Storage.Mongo;
```
Configure services:
```cs
services.AddEmailTemplateLibraryServices(new TemplateDashboardOptions()
{
            IgnoreAntiforgeryToken = true,
            LoadBaseTemplates = true
}).AddMongoStorage(new MongoStorageOptions()
{
            UrlConnection = Configuration.GetValue<string>("ConnectionStrings.MainConnection")
});
```

SQL / Postgres
Add a reference to EmailTemplateLibrary.Storage.Sql or EmailTemplateLibrary.Storage.Postgres and in your Startup.cs:

```cs
using EmailTemplateLibrary.Storage.Postgres;
```
Configure services:
```cs
services
.AddEmailTemplateLibraryServices(new EmailTemplateLibrary.Dashboard.TemplateDashboardOptions())
.AddPostgresStorage(new PostgreStorageOptions
{
            ConnectionString = connString
});

var templateStore = serviceProvider.GetService<TemplateStorage>();			
templateStore.CreateBaseTemplates(); //optional

```
See samples for .Net 5 Console and AspnetCore

## UI

Once running, if you installed the ASPNetCore package you will be able to access the url "http://yoururl/templates".

New:

![image](https://user-images.githubusercontent.com/1830594/136530424-744e18b6-5f02-4dd5-8b72-1d15b9a67c93.png)

Edit:

![image](https://user-images.githubusercontent.com/1830594/136530304-4567620b-e367-453b-8730-e48002f129e6.png)


## TODO
- [X] Sample project showing usage
- [ ] Default Templates
- [X] Mongo Storage
- [X] SQL Storage
- [ ] Override Styles
