﻿using System;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using EmailTemplateLibrary.Dashboard;
using Microsoft.AspNetCore.Http;

namespace EmailTemplateLibrary.AspNetCore.Dashboard
{
    internal sealed class AspNetCoreDashboardResponse : DashboardResponse
    {
        private readonly HttpContext _context;

        public AspNetCoreDashboardResponse(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public override string ContentType
        {
            get { return _context.Response.ContentType; }
            set { _context.Response.ContentType = value; }
        }

        public override int StatusCode
        {
            get { return _context.Response.StatusCode; }
            set { _context.Response.StatusCode = value; }
        }

        public override Stream Body => _context.Response.Body;

        public override Task WriteAsync(string text)
        {
            return _context.Response.WriteAsync(text);
        }

        public override void SetExpire(DateTimeOffset? value)
        {
            _context.Response.Headers["Expires"] = value?.ToString("r", CultureInfo.InvariantCulture);
        }
    }
}
