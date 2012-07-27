using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc.Html
{
    public static class ViewHelpers
    {
        public static MvcHtmlString DisplayList<T>(this HtmlHelper html, IEnumerable<T> list)
        {
            var a = String.Join(",", list);
            return new MvcHtmlString(a);
        }

        public static MvcHtmlString ShowErrorMessage(this HtmlHelper html)
        {
            if(html.ViewBag.ErrorMessage == null)
                return new MvcHtmlString(string.Empty);
            return new MvcHtmlString(String.Format("<div style='color:red'>{0}</div>", html.ViewBag.ErrorMessage));
        }
    }
}