using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Training_wmqr.Models;

namespace Training_wmqr.Infrastructure
{
    public static class HttpUser
    {
        public static string GetUsername()
        {
            var user = new CurrentUser();
            return user.UserName();
        }

        public static bool IsFavourite(this Document document, string username)
        {
            return document.Favourites.Any(f => f.User.Username == username);
        }
    }
}