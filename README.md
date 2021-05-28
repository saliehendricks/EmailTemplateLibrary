# Email Template Library

## Build Status

## Overview
An easy to store and modify string/html based templates for emails, sms' or anything else that requires a template.

Storage options:
- File
- SQL
- Mongo
- Redis

# Installation

```
PM> Install-Package EmailTemplateLibrary
```
Then update your Startup.cs file:

```cs
public void Configuration(IAppBuilder app)
{
    app.UseEmailTemplateLibrary(); // Defaults to file system
}
```

## TODO
[ ] Sample Email generation
[ ] Default Templates
[ ] Default Styles
[ ] Override Styles