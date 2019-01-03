using Btp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class RecrutementController : Controller
    {
        ModelDBContext mdbc = new ModelDBContext();

        // GET: Recrutement
        public ActionResult Index()
        {
            var rec = from recru in mdbc.Recrutementinfo select recru;
            return View(rec.ToList());
        }
        public ActionResult SearchIndex(String str)
        {
            
            IQueryable<Recrutement> rec;
            if (str=="tout")
            {
                rec = from recru in mdbc.Recrutementinfo select recru;
                ViewBag.Titre ="Liste des offres d'emploi et de stage";
            }
            else
            {
                TypeOffre typeoffre = str == "emploi" ? TypeOffre.Emploi : TypeOffre.Stage;
                ViewBag.Titre = typeoffre == TypeOffre.Emploi ? "Liste des offres d'emploi" : "Liste des offres de stage";
                rec = from recru in mdbc.Recrutementinfo where recru.Type == typeoffre select recru;
            }
            
            
            return View(rec.ToList());
        }
        
        // GET: Recrutement/Details/5
        public ActionResult Details(int? id)
        {
            //TODO design details view
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Recrutement rec = mdbc.Recrutementinfo.Find(id);
                ViewBag.Details = rec.Description;
                return View(rec);
            }
            
        }

        // GET: Recrutement/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Recrutement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Recrutement rec)
        {
            try
            {
                
                rec.DatePublication = DateTime.UtcNow;
                mdbc.Recrutementinfo.Add(rec);
                mdbc.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Erreur = "Une erreur est survenue lors de l'enrégistrement. Veuillez réessayez s'il vous plaît";
                return View();
            }
        }

        // GET: Recrutement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            Recrutement rec = mdbc.Recrutementinfo.Find(id);
           
            return View(rec);
        }

        // POST: Recrutement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Recrutement rec)
        {
            try
            {
                if (id == null)
                    return RedirectToAction("Index");
                
                Recrutement recrutement = mdbc.Recrutementinfo.Find(id);
                recrutement.Description = rec.Description;
                //recrutement.LieuDepot = rec.LieuDepot;
                recrutement.Niveau = rec.Niveau;
                recrutement.Post = rec.Post;
                recrutement.Type = rec.Type;
                recrutement.DateExpiration = rec.DateExpiration;

                mdbc.SaveChanges();
                
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Erreur = "Une erreur est survenue lors de la mise à jour des des informations. réessayez s'il vous plaît";
                return View();
            }
        }

        // GET: Recrutement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Recrutement rec = mdbc.Recrutementinfo.Find(id);
            if(rec==null)
                return RedirectToAction("Index");
            return View(rec);
        }

        // POST: Recrutement/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Recrutement rec = mdbc.Recrutementinfo.Find(id);
                mdbc.Recrutementinfo.Remove(rec);
                mdbc.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Erreur = "Impossible de supprimer l'élément selectionné";
                return View();
            }
        }
    }
}
