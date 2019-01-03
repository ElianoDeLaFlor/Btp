using Btp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class ConnectionController : Controller
    {
        ModelDBContext mdbc = new ModelDBContext();
        Users user = new Users();
        // GET: Connection
        public ActionResult Index()
        {
            return RedirectToAction("Connexion");
        }
        //GET: Connection/Create
        public ActionResult Create()
        {
            return RedirectToAction("Connexion");
        }
        //GET: Connection/Connexion
        public ActionResult Connexion()
        {
            return View();
        }
        //POST: Connection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion(Connection con)
        {
            if (IsCredentialCorrect(con))
            {
                Session["Users"] = user;
                return RedirectToAction("Index", "User");
            }
            else
            {
                //Session["Users"] = null;
                ViewBag.Info = "Les informations saisies sont incorrectes";
                return View();
            }
        }

        private Boolean IsCredentialCorrect(Connection con)
        {
            user = GetUserByLogin(con.UserLogin);
            if (con.UserLogin == null)
                return false;
            if (user != null)
            {
                if (con.Password != null)
                {
                    if (con.Password.Length > 0)
                        return user.Password.Equals(Crypto.Hash(con.Password));
                    return false;
                }
                return false;
            }
            return false;
        }

        private Users GetUserByLogin(string login)
        {
            var user = from u in mdbc.Usersinfo where u.Login == login select u;
            Users utilisateur = new Users();
            if (user.Count() > 0)
            {
                foreach (var item in user)
                {
                    utilisateur = item;
                }

                return utilisateur;
            }
            else
            {
                return null;
            }
        }

    }
}