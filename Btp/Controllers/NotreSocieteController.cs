using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class NotreSocieteController : Controller
    {
        // GET: NotreSociete
        public ActionResult Index()
        {
            return View();
        }

        // GET: NotreSociete/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotreSociete/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotreSociete/Create
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

        // GET: NotreSociete/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotreSociete/Edit/5
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

        // GET: NotreSociete/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotreSociete/Delete/5
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
