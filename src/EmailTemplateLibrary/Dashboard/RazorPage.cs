// This file is adapted the from Hangfire library.
// Author Attribution: Sergey Odinokov.
// License along with Hangfire. See <http://www.gnu.org/licenses/>.

using EmailTemplateLibrary.Storage;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace EmailTemplateLibrary.Dashboard
{
    public abstract class RazorPage
    {
        private readonly StringBuilder _content = new StringBuilder();
        private string _body;

        protected RazorPage()
        {
            GenerationTime = Stopwatch.StartNew();
            Html = new HtmlHelper(this);
        }

        public RazorPage Layout { get; protected set; }
        public HtmlHelper Html { get; private set; }
        public UrlHelper Url { get; private set; }

        public TemplateStorage Storage => Context.Storage;
        public string AppPath => Context.Options.AppPath;
        public DashboardOptions DashboardOptions => Context.Options;
        public Stopwatch GenerationTime { get; private set; }

        public DashboardContext Context { get; private set; }

        internal DashboardRequest Request => Context.Request;
        internal DashboardResponse Response => Context.Response;

        public string RequestPath => Request.Path;

        public abstract void Execute();

        public string Query(string key)
        {
            return Request.GetQuery(key);
        }

        public override string ToString()
        {
            return TransformText(null);
        }

        public void Assign(RazorPage parentPage)
        {
            Context = parentPage.Context;
            Url = parentPage.Url;

            GenerationTime = parentPage.GenerationTime;
            //_statisticsLazy = parentPage._statisticsLazy;
        }

        internal void Assign(DashboardContext context)
        {
            Context = context;
            Url = new UrlHelper(context);

            //_statisticsLazy = new Lazy<StatisticsDto>(() =>
            //{
            //    var monitoring = Storage.GetMonitoringApi();
            //    return monitoring.GetStatistics();
            //});
        }

        /// <exclude />
        protected void WriteLiteral(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
                return;
            _content.Append(textToAppend);
        }

        /// <exclude />
        protected virtual void Write(object value)
        {
            if (value == null)
                return;
            var html = value as NonEscapedString;
            WriteLiteral(html?.ToString() ?? Encode(value.ToString()));
        }

        protected virtual object RenderBody()
        {
            return new NonEscapedString(_body);
        }

        private string TransformText(string body)
        {
            _body = body;
            
            Execute();
            
            if (Layout != null)
            {
                Layout.Assign(this);
                return Layout.TransformText(_content.ToString());
            }
            
            return _content.ToString();
        }

        private static string Encode(string text)
        {
            return string.IsNullOrEmpty(text)
                       ? string.Empty
                       : WebUtility.HtmlEncode(text);
        }
    }
}
