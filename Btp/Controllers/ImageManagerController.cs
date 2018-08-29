using Btp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class ImageManagerController : Controller
    {
        ModelDBContext mdbc = new ModelDBContext();
        // GET: ImageManager
        public ActionResult Index()
        {
            var lst = from img in mdbc.ImageManagerinfo select img;
            return View(lst);
        }

        // GET: ImageManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ImageManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageManager imageManager,HttpPostedFileBase httpPostedFileBase)
        {
            try
            {
                imageManager.Chemin = httpPostedFileBase.FileName;
               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void CreateFolderIfNotExist(string dirname)
        {
            if (!Directory.Exists(dirname))
                Directory.CreateDirectory(dirname);
        }

        // GET: ImageManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ImageManager/Edit/5
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

        // GET: ImageManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ImageManager/Delete/5
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
