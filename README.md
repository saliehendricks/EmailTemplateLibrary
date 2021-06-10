# Email Template Library

[![Nuget](https://github.com/saliehendricks/EmailTemplateLibrary/actions/workflows/publish.yml/badge.svg)](https://github.com/saliehendricks/EmailTemplateLibrary/actions/workflows/publish.yml)

## Overview
An easy way to add string based templates to your dotnet application. Store and modify string or html based templates for emails, sms' or anything else that requires a template.

![image](https://user-images.githubusercontent.com/1830594/120047159-35d59200-c014-11eb-9448-88244a623027.png)

Storage options:
- File: Stores templates on disk. Suitable for single site web applications or service.
- Mongo: Stores templates in MongoDB
- SQL - TODO

# Base Templates
Optionally include base templates:
- NewUserActivationEmail
- NewUserAdminActivationEmail
- PasswordResetEmail

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

## TODO
- [X] Sample project showing usage
- [ ] Default Templates
- [X] Mongo Storage
- [ ] SQL Storage
- [ ] Override Styles
