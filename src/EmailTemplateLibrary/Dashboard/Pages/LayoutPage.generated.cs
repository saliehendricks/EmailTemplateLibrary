﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmailTemplateLibrary.Dashboard.Pages
{
    
    #line 2 "..\..\Dashboard\Pages\LayoutPage.cshtml"
    using System;
    
    #line default
    #line hidden
    using System.Collections.Generic;
    
    #line 3 "..\..\Dashboard\Pages\LayoutPage.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
    using System.Linq;
    
    #line 4 "..\..\Dashboard\Pages\LayoutPage.cshtml"
    using System.Reflection;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 5 "..\..\Dashboard\Pages\LayoutPage.cshtml"
    using EmailTemplateLibrary.Dashboard;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class LayoutPage : RazorPage
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






WriteLiteral(@"<!DOCTYPE html>

<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <title>Email Template Library</title>
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js""></script>
    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js""></script>
");


            
            #line 17 "..\..\Dashboard\Pages\LayoutPage.cshtml"
       var version = GetType().GetTypeInfo().Assembly.GetName().Version; 

            
            #line default
            #line hidden
WriteLiteral("</head>\r\n<body>\r\n    <div id=\"wrap\">\r\n        <div class=\"container\" style=\"margi" +
"n-bottom: 20px;\">\r\n            ");


            
            #line 22 "..\..\Dashboard\Pages\LayoutPage.cshtml"
       Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral(@"
        </div>
    </div>
    <div id=""footer"">
        <div class=""container"">
            <ul class=""list-inline credit"">
                <li>
                    <a href="""" target=""_blank"" rel=""noopener noreferrer"">
                        Email Template Library v<span>");


            
            #line 30 "..\..\Dashboard\Pages\LayoutPage.cshtml"
                                                 Write(VersionText);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n                    </a>\r\n                </li>\r\n                <li>");


            
            #line 33 "..\..\Dashboard\Pages\LayoutPage.cshtml"
               Write(Storage);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n                <li>");


            
            #line 34 "..\..\Dashboard\Pages\LayoutPage.cshtml"
               Write(Html.LocalTime(DateTime.UtcNow));

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n            </ul>\r\n        </div>\r\n        <div id=\"templateConfig\"\r\n     " +
"        data-baseurl=\"");


            
            #line 38 "..\..\Dashboard\Pages\LayoutPage.cshtml"
                       Write(Url.To("/"));

            
            #line default
            #line hidden
WriteLiteral("\">\r\n        </div>\r\n    </div>\r\n    \r\n    <script src=\"");


            
            #line 42 "..\..\Dashboard\Pages\LayoutPage.cshtml"
            Write(Url.To($"/js{version.Major}{version.Minor}{version.Build}0"));

            
            #line default
            #line hidden
WriteLiteral("\"></script>\r\n</body>\r\n</html>");


        }
    }
}
#pragma warning restore 1591
