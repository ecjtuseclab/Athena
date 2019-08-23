using Athena.common.Util.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    public static class StaticContentExtension
    {
        public static IHtmlString RefStyle(this HtmlHelper htmlHelper, string contentPath)
        {
            string contentUrl = GenerateContentUrl(contentPath, htmlHelper.ViewContext.HttpContext);
            string html = string.Format("<link rel=\"stylesheet\" href=\"{0}\" >", contentUrl);
            return new HtmlString(html);
        }

        public static IHtmlString RefScript(this HtmlHelper htmlHelper, string contentPath)
        {
            string contentUrl = GenerateContentUrl(contentPath, htmlHelper.ViewContext.HttpContext);
            string html = string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", contentUrl);
            return new HtmlString(html);
        }

        static string GenerateContentUrl(string contentPath, HttpContextBase httpContext)
        {
            if (String.IsNullOrEmpty(contentPath))
            {
                throw new ArgumentException("ContentPath can not by null.");
            }

            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (contentPath[0] == '~')
            {
                return PathHelpers.GenerateClientUrl(httpContext, contentPath);
            }
            else
            {
                return contentPath;
            }
        }
    }
}
