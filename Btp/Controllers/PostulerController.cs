using Btp.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Postuler post,HttpPostedFileBase cv, HttpPostedFileBase lettre, HttpPostedFileBase[] attest)
        {
            try
            {
                if(cv.ContentLength>1000000||lettre.ContentLength>1000000)
                {
                    ViewBag.Message = "La taille du cv ou de la lettre ne doit pas excéder 1MB";
                    return View();
                }
                
                if(cv!=null || lettre!=null || attest!=null)
                {
                    //files are provided
                    //Cv
                    CreateFolderIfNotExist(Server.MapPath("~/Post/CV"));
                    post.CheminCv = "/Post/CV/" + cv.FileName;

                    //lettre
                    CreateFolderIfNotExist(Server.MapPath("~/Post/Lettre"));
                    post.CheminCv = "/Post/Lettre/" + lettre.FileName;

                    //attestation
                    CreateFolderIfNotExist(Server.MapPath("~/Post/Attestation"));
                    string attestfiles = "";
                    int cnt = 1;
                    foreach (var item in attest)
                    {
                        if(cnt==attest.Length)
                            attestfiles += "/Post/Attestation/"+item.FileName;
                        else
                            attestfiles += "/Post/Attestation/"+item.FileName + ",";
                        cnt++;
                    }
                    post.Attestation= attestfiles;

                    //save file on disk
                    if(Save(cv,lettre,attest,post))
                    {
                        mdbc.Postulerinfo.Add(post);
                        mdbc.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Effacer(post.CheminCv, post.Lettre, post.Attestation);
                    }
                    return View();
                }
                else
                {
                    //some files are missing
                    ViewBag.Message = "Renseignez tous les documents s'il vous plaît";
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }
        private bool Save(HttpPostedFileBase filecv, HttpPostedFileBase filelettre, HttpPostedFileBase[] fileatt,Postuler postuler)
        {
            bool a, b, c;
            a = SaveImg(filecv, postuler.CheminCv);
            b = SaveImg(filelettre, postuler.Lettre);
            c= a = SaveImg(fileatt, postuler.Attestation);
            if (a && b && c)
                return true;
            else
                return false;
        }

        private bool SaveImg(HttpPostedFileBase fileBase, string chemin)
        {
            try
            {
                string p = Server.MapPath("~" + chemin);
                fileBase.SaveAs(p);
                return true;
            }
            catch
            {
                return false;
            }

        }

        private bool SaveImg(HttpPostedFileBase[] fileBase, string chemin)
        {
            try
            {
                String[] paths = chemin.Split(',');
                for (int i = 0; i < paths.Length; i++)
                {
                    string p = Server.MapPath("~" + paths[i]);
                    fileBase[i].SaveAs(p);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void CreateFolderIfNotExist(string dirname)
        {
            if (!Directory.Exists(dirname))
                Directory.CreateDirectory(dirname);
        }

        private void DeleteOnError(string path)
        {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
        private void Effacer(string cv,string lettre,string attestation)
        {
            DeleteOnError(cv);
            DeleteOnError(lettre);
            string[] att = attestation.Split(',');
            foreach (var item in att)
            {
                DeleteOnError(item);
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
