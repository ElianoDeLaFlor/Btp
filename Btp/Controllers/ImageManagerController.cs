using Btp.Models;
using RTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    [AuthUsers(AccessLevelOne =Role.Sous_Administrateur,AccessLevelTwo =Role.Administrateur)]
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
        public ActionResult Details(int? id)
        {
            return RedirectToAction("Index");
        }

        // GET: ImageManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageManager imageManager,HttpPostedFileBase up1)
        {
            try
            {
                if (up1 != null)
                {
                    CreateFolderIfNotExist(Server.MapPath("~/ImgBoard"));
                    imageManager.Chemin = "/ImgBoard/" + up1.FileName;
                    mdbc.ImageManagerinfo.Add(imageManager);
                    if (SaveImg(up1, imageManager.Chemin))
                    {
                        mdbc.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.Message = "Une erreur est survenue lors de l'enrégistrement, réessayez s'il vous plaît";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Selectionnez une image et réessayez s'il vous plaît";
                    return View();
                }
            }
            catch
            {
                ViewBag.Message = "Une erreur est survenue lors de l'enrégistrement, réessayez s'il vous plaît";
                return View();
            }
        }

        private void CreateFolderIfNotExist(string dirname)
        {
            if (!Directory.Exists(dirname))
                Directory.CreateDirectory(dirname);
        }
        private bool SaveImg(HttpPostedFileBase fileBase,string chemin)
        {
            try
            {
                string p = Server.MapPath("~"+chemin);
                fileBase.SaveAs(p);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        private bool DeleteExitingImage(string path)
        {
            if(System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(Server.MapPath("~"+path));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
            
        }
        // GET: ImageManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            ImageManager img = mdbc.ImageManagerinfo.Find(id);
            return View(img);
        }

        // POST: ImageManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ImageManager manager,HttpPostedFileBase up1)
        {
            if (id == null)
                return RedirectToAction("Index");
            try
            {
                ImageManager imageManager = mdbc.ImageManagerinfo.Find(id);
                if (up1 != null)
                {
                    //new image changed
                    if (DeleteExitingImage(imageManager.Chemin))
                    {
                        CreateFolderIfNotExist(Server.MapPath("~/ImgBoard"));
                        imageManager.Chemin = "/ImgBoard/" + up1.FileName;
                        imageManager.Title = manager.Title;
                        imageManager.Description = manager.Description;
                        if (SaveImg(up1, imageManager.Chemin))
                        {
                            mdbc.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        ViewBag.Message = "Une erreur est survenue lors de l'enrégistrement, réessayez s'il vous plaît";
                        return View();

                    }
                    ViewBag.Message = "Oops,une erreur est survenue réessayez s'il vous plaît";
                    return View();
                }
                else
                {
                    //image not changed
                    try
                    {
                        imageManager.Title = manager.Title;
                        imageManager.Description = manager.Description;
                        mdbc.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        ViewBag.Message = "Une erreur est survenue lors de l'enrégistrement, réessayez s'il vous plaît";
                        return View();
                    }
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ImageManager/Delete/5
        public ActionResult Delete(int? id)
        {
            return RedirectToAction("Index");
        }

        // POST: ImageManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            return RedirectToAction("Index");
        }
    }
}
