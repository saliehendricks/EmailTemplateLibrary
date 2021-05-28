// This file is adapted the from Hangfire library.
// Author Attribution: Sergey Odinokov.
// License along with Hangfire. See <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EmailTemplateLibrary.Dashboard
{
    public class UrlHelper
    {
        private readonly DashboardContext _context;

        public UrlHelper(DashboardContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public string To(string relativePath)
        {
            return _context.Options.PrefixPath +
                   (
                       _context.Request.PathBase
                       + relativePath
                   );
        }

        public string Home()
        {
            return To("/");
        }
    }
    public class HtmlHelper
    {
        private static readonly Type DisplayNameType;
        private static readonly Func<object, string> GetDisplayName;

        private readonly RazorPage _page;

        static HtmlHelper()
        {
            try
            {
#if !NETSTANDARD1_3
                DisplayNameType = typeof(DisplayNameAttribute);
#else
                DisplayNameType = Type.GetType("System.ComponentModel.DisplayNameAttribute, System.ComponentModel.Primitives");
#endif
                if (DisplayNameType == null) return;

                var p = Expression.Parameter(typeof(object));
                var converted = Expression.Convert(p, DisplayNameType);

                GetDisplayName = Expression.Lambda<Func<object, string>>(Expression.Call(converted, "get_DisplayName", null), p).Compile();
            }
            catch
            {
                // Ignoring
            }
        }

        public HtmlHelper(RazorPage page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            _page = page;
        }

        //public NonEscapedString Breadcrumbs(string title,  IDictionary<string, string> items)
        //{
        //    if (items == null) throw new ArgumentNullException(nameof(items));
        //    return RenderPartial(new Breadcrumbs(title, items));
        //}

        //public NonEscapedString JobsSidebar()
        //{
        //    return RenderPartial(new SidebarMenu(JobsSidebarMenu.Items));
        //}

        //public NonEscapedString SidebarMenu( IEnumerable<Func<RazorPage, MenuItem>> items)
        //{
        //    if (items == null) throw new ArgumentNullException(nameof(items));
        //    return RenderPartial(new SidebarMenu(items));
        //}

        //public NonEscapedString BlockMetric( DashboardMetric metric)
        //{
        //    if (metric == null) throw new ArgumentNullException(nameof(metric));
        //    return RenderPartial(new BlockMetric(metric));
        //}

        //public NonEscapedString InlineMetric( DashboardMetric metric)
        //{
        //    if (metric == null) throw new ArgumentNullException(nameof(metric));
        //    return RenderPartial(new InlineMetric(metric));
        //}

        //public NonEscapedString Paginator( Pager pager)
        //{
        //    if (pager == null) throw new ArgumentNullException(nameof(pager));
        //    return RenderPartial(new Paginator(pager));
        //}

        //public NonEscapedString PerPageSelector( Pager pager)
        //{
        //    if (pager == null) throw new ArgumentNullException(nameof(pager));
        //    return RenderPartial(new PerPageSelector(pager));
        //}

        public NonEscapedString RenderPartial(RazorPage partialPage)
        {
            partialPage.Assign(_page);
            return new NonEscapedString(partialPage.ToString());
        }

        public NonEscapedString Raw(string value)
        {
            return new NonEscapedString(value);
        }        

        public NonEscapedString RelativeTime(DateTime value)
        {
            return Raw($"<span data-moment=\"{HtmlEncode(value.ToTimestamp().ToString(CultureInfo.InvariantCulture))}\">{HtmlEncode(value.ToString(CultureInfo.CurrentUICulture))}</span>");
        }

        public NonEscapedString MomentTitle(DateTime time, string value)
        {
            return Raw($"<span data-moment-title=\"{HtmlEncode(time.ToTimestamp().ToString(CultureInfo.InvariantCulture))}\">{HtmlEncode(value)}</span>");
        }

        public NonEscapedString LocalTime(DateTime value)
        {
            return Raw($"<span data-moment-local=\"{HtmlEncode(value.ToTimestamp().ToString(CultureInfo.InvariantCulture))}\">{HtmlEncode(value.ToString(CultureInfo.CurrentUICulture))}</span>");
        }

        public string ToHumanDuration(TimeSpan? duration, bool displaySign = true)
        {
            if (duration == null) return null;

            var builder = new StringBuilder();
            if (displaySign)
            {
                builder.Append(duration.Value.TotalMilliseconds < 0 ? "-" : "+");
            }

            duration = duration.Value.Duration();

            if (duration.Value.Days > 0)
            {
                builder.Append($"{duration.Value.Days}d ");
            }

            if (duration.Value.Hours > 0)
            {
                builder.Append($"{duration.Value.Hours}h ");
            }

            if (duration.Value.Minutes > 0)
            {
                builder.Append($"{duration.Value.Minutes}m ");
            }

            if (duration.Value.TotalHours < 1)
            {
                if (duration.Value.Seconds > 0)
                {
                    builder.Append(duration.Value.Seconds);
                    if (duration.Value.Milliseconds > 0)
                    {
                        builder.Append($".{duration.Value.Milliseconds.ToString().PadLeft(3, '0')}");
                    }

                    builder.Append("s ");
                }
                else
                {
                    if (duration.Value.Milliseconds > 0)
                    {
                        builder.Append($"{duration.Value.Milliseconds}ms ");
                    }
                }
            }

            if (builder.Length <= 1)
            {
                builder.Append(" <1ms ");
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }       

        public string HtmlEncode(string text)
        {
            return WebUtility.HtmlEncode(text);
        }
    }
}
