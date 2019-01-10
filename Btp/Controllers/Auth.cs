using Btp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class AuthUserAttribute : AuthorizeAttribute
    {
        public Role AccessLevel { get; set; }
        //public object Utilisateur { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);
            //if (Utilisateur == null)
            //    return false;
            //Users users = (Users)Utilisateur;
            //return AccessLevel == users.UserRole;
            return AccessLevel == Role.Administrateur;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        controller = "Connection",
                        action = "Connexion"
                    }
                    ));
        }
    }

   
}