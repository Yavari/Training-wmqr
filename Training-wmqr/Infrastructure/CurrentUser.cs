using System.Text.RegularExpressions;
using System.Web;

namespace Training_wmqr.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        public virtual string Name()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public virtual string UserName()
        {
            return GetUsername(Name());
        }

        public bool IsLoggedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static string GetUsername(string login)
        {
            if (string.IsNullOrEmpty(login)) return "";
            return (Regex.Match(login, @"(BWCORG\\)?(.*)").Groups[2].Value).ToLower();
        }
    }
}