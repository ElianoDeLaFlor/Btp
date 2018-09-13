using Btp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class PostulerController : Controller
    {
        ModelDBContext mdbc = new ModelDBContext();
        // GET: Postuler
        public ActionResult Index()
        {
            var lst = from post in mdbc.Postulerinfo select post;
            return View(lst.ToList());
        }

        // GET: Postuler/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Postuler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Postuler/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Postuler/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Postuler/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Postuler/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Postuler/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
