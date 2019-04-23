using Btp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Btp.Controllers
{
    [AuthConnection]
    public class PasseChangeController : Controller
    {
        ModelDBContext mdbc = new ModelDBContext();
        // GET: PasseChange
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        // GET: PasseChange/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction("Create");
        }

        // GET: PasseChange/Create
        [AuthConnection]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PasseChange/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthConnection]
        public ActionResult Create(PassChange pass)
        {
            try
            {
                if(OldPassLoginIsOk(pass.UserLogin,pass.OldPass))
                {
                    //Login and pass are correct
                    int id = ChangeId(pass.UserLogin);
                    var user = from u in mdbc.Usersinfo where u.ID == id select u;
                    foreach (var item in user)
                    {
                        item.ConfirmPassword = Crypto.Hash(pass.ConfPass);
                        item.Password = Crypto.Hash(pass.NewPass);
                    }
                    mdbc.SaveChanges();
                    ViewBag.Info = "Modification effectuée avec succès";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    //Login or pass is wrong
                    ViewBag.Error = "Le nom d'utilisateur ou le mot de passe est incorrecte";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PasseChange/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Create");
        }

        // POST: PasseChange/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return RedirectToAction("Create");
        }

        // GET: PasseChange/Delete/5
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Create");
        }

        // POST: PasseChange/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return RedirectToAction("Create");
        }

        private int ChangeId(string login)
        {
            var user = from u in mdbc.Usersinfo where u.Login == login select u;
            int id = 0;
            foreach (var item in user)
            {
                id = item.ID;
            }
            return id;
        }
        private bool OldPassLoginIsOk(string login, string old)
        {
            string hashpass = Crypto.Hash(old);
            var user = from u in mdbc.Usersinfo where u.Login == login && u.Password == hashpass select u;
            return user.Count() > 0 ? true : false;
        }
        private bool ConfirmationIsOk(string str, string str2)
        {
            string hash1 = Crypto.Hash(str);
            string hash2 = Crypto.Hash(str2);
            return hash1 == hash2;
        }
    }
    
        
}
