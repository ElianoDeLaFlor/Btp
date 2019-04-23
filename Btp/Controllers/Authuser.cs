using Btp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class AuthUserAttribute: AuthorizeAttribute
    {
        ModelDBContext mdbc = new ModelDBContext();
        public Role AccessLevel { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Users user = (Users)HttpContext.Current.Session["users"];
            HttpContext.Current.Session["url"] = httpContext.Request.RawUrl;
            if (user == null)
            {
                HttpContext.Current.Session["Info"] = "Vous devez vous connecter pour accéder à cette page.";
                return false;
            }
            else
            {
                return AccessLevel == user.UserRole;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            if (HttpContext.Current.Session["users"] == null)
            {

                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "Connection",
                            action = "Connexion"
                        }
                        ));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        controller = "Connection",
                        action = "NotAuthorized"
                    }
                    ));
            }
        }
    }
}