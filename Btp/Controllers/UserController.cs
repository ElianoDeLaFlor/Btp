using Btp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Btp.Controllers
{
    [AuthUser(AccessLevel = Role.Administrateur)]
    public class UserController : Controller
    {
        ModelDBContext mdbc = new ModelDBContext();

        // GET: User
        
        public ActionResult Index()
        {
            //ViewBag.test = "testttt";
            //if (!Auth())
            //{
            //    //ViewBag.Info = "Vous devez vous connecter pour accéder à cette page.";
            //    return RedirectToAction("Connexion", "Connection");
            //}
            //else
            {
                Users users = (Users)Session["Users"];
                if (users.UserRole == Role.Administrateur)
                {
                    var lst = from user in mdbc.Usersinfo select user;
                    return View(lst);
                }
                else
                {
                    return RedirectToAction("NotAuthorized", "Connection");
                }
            }

        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            //if (Auth())
            {
                if (id == null)
                    return RedirectToAction("Index");
                var user = mdbc.Usersinfo.SingleOrDefault(u => u.ID == id);
                if (user == null)
                    return RedirectToAction("Index");
                return View(user);
            }
            //return RedirectToAction("Connexion", "Connection");
        }

        // GET: User/Create
        public ActionResult Create()
        {
            //if (Auth())
                return View();
            //return RedirectToAction("Connexion", "Connection");
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            try
            {

                // TODO: Add insert logic here
                if (!UserExist(users.Login))
                {
                    //User doen't exist
                    users.ConfirmPassword = Crypto.Hash(users.ConfirmPassword);
                    users.Password = Crypto.Hash(users.Password);
                    mdbc.Usersinfo.Add(users);
                    mdbc.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    //User exist
                    ViewBag.Exist = "Ce nom d'utilisateur est déja pris!";
                    return View();

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (Auth())
            {

                if (id == null)
                    return RedirectToAction("Index");
                Users user = mdbc.Usersinfo.Find(id);
                if (user == null)
                    return RedirectToAction("Index");
                return View(user);
            }
            //return RedirectToAction("Connexion", "Connection");
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Users users)
        {
            //if (Auth())
            {

            if (Update(id, users))
                return RedirectToAction("Index");
            else
                return View();
            }
            //return RedirectToAction("Connexion", "Connection");
        }

        private bool Update(int id, Users users)
        {
            try
            {
                var u = from user in mdbc.Usersinfo where user.ID == id select user;
                foreach (var item in u)
                {
                    item.ConfirmPassword = item.Password;
                    item.LastName = users.LastName;
                    item.Login = users.Login;
                    item.Name = users.Name;
                    item.UserRole = users.UserRole;
                }
                mdbc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool UpdatePasse(int id, Users users)
        {
            try
            {
                var u = from user in mdbc.Usersinfo where user.ID == id select user;
                foreach (var item in u)
                {
                    item.ConfirmPassword = Crypto.Hash(users.ConfirmPassword);
                    item.Password = Crypto.Hash(users.Password);
                }
                mdbc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool UserExist(string userlogin)
        {
            var user = from u in mdbc.Usersinfo where u.Login == userlogin select u;
            return user.Count() > 0 ? true : false;
        }
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            //var user = mdbc.Usersinfo.SingleOrDefault(u => u.ID == id);
            Users users = mdbc.Usersinfo.Find(id);
            return View(users);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Users users)
        {

            try
            {
                Users user = mdbc.Usersinfo.Find(id);
                mdbc.Usersinfo.Remove(user);
                mdbc.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
