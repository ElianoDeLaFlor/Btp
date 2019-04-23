using Btp.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    
    public class PostulerController : Controller
    {
        ModelDBContext mdbc = new ModelDBContext();
        public string[] Critere { get; set; }
        // List<Postuler> list;
        // GET: Postuler
        /*public ActionResult Index(string searching)
        {
            if (searching == null ||searching.Length==0)
            {
                list = GetList();
                ViewBag.Search = null;
            }
            else
            {
                list = GetSearchList(searching);
                if (list.Count() == 0)
                    ViewBag.Search = "Il n'ya aucun résultat correspondant à votre recherche";
                else
                    ViewBag.Search = "Il y a " + list.Count() + " résultat(s) correspondant à votre recherche";
            }
            
            return View(list);
        }*/

        /*public ActionResult Cancel()
        {
            return Index("");
        }*/
        [AuthUsers(AccessLevelOne = Role.Administrateur, AccessLevelTwo = Role.Cv_Administrateur)]
        public ActionResult Index()
        {
            var lst = GetList();
            ViewBag.Postuler = lst;
            return View(lst);
        }
        [AuthUsers(AccessLevelOne = Role.Administrateur, AccessLevelTwo = Role.Cv_Administrateur)]
        public ActionResult Recherche(string searchtext)
        {
            var lst = GetSearchList(searchtext);
            return PartialView("SearchResult", lst);
        }
        public string test(string test)
        {
            return ViewBag.test = test;
        }
        private FileResult Download(string chemin)
        {
            /*if (System.IO.File.Exists(chemin))
            {
                byte[] filebytes = System.IO.File.ReadAllBytes(chemin);
                FileInfo fi = new FileInfo(chemin);
                ZipFile.
                string filename = fi.Name;
                return File(filebytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            }*/
            return null;
            
        }
        private List<string> Chemin(string id)
        {
            if (IsNumeric(id))
            {
                int idd = int.Parse(id);
                Postuler postuler = mdbc.Postulerinfo.Find(idd);
                List<string> str = new List<string>();
                str.Add(postuler.CheminCv);
                str.Add(postuler.Lettre);
                string[] att = postuler.Attestation.Split(',');
                foreach (var item in att)
                {
                    str.Add(item);
                }
                return str;
            }
            return null;
        }
        [AuthUsers(AccessLevelOne = Role.Administrateur, AccessLevelTwo = Role.Cv_Administrateur)]
        public FileResult[] Telechargement(string id)
        {
            List<string> str = Chemin(id);
            FileResult[] lfr=new FileResult[str.Count];
            int cnt = 0;
            try
            {
                foreach (var item in str)
                {

                    lfr[cnt]=(Download(Server.MapPath("~" + item)));
                    cnt++;
                }
                Session["frr"] = lfr;
                return lfr;
            }
            catch
            {
                return lfr;
            }

        }
        List<Postuler> GetList()
        {
            return (from post in mdbc.Postulerinfo select post).ToList();
        }
        public FileResult[] DownTest(FileResult[] fr)
        {
            FileResult[] frr = (FileResult[])Session["frr"];
            
            return frr;
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
        //GET: Postuler/Demander
        public ActionResult Demander(int? recid, string post)
        {
            ViewBag.post = post;
            return View();
        }


        // POST: Postuler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Postuler post, HttpPostedFileBase cv, HttpPostedFileBase lettre, HttpPostedFileBase[] attest)
        {
            try
            {
                if (cv.ContentLength > 5000000 || lettre.ContentLength > 5000000)
                {
                    ViewBag.Message = "La taille du cv ou de la lettre ne doit pas excéder 5MB";
                    return View();
                }

                if (cv != null || lettre != null || attest != null)
                {
                    //files are provided
                    //Cv
                    CreateFolderIfNotExist(Server.MapPath("~/Post/CV"));
                    post.CheminCv = "/Post/CV/" + cv.FileName;

                    //lettre
                    CreateFolderIfNotExist(Server.MapPath("~/Post/Lettre"));
                    post.Lettre = "/Post/Lettre/" + lettre.FileName;

                    //attestation
                    CreateFolderIfNotExist(Server.MapPath("~/Post/Attestation"));
                    string attestfiles = "";
                    int cnt = 1;
                    foreach (var item in attest)
                    {
                        if (cnt == attest.Length)
                            attestfiles += "/Post/Attestation/" + item.FileName;
                        else
                            attestfiles += "/Post/Attestation/" + item.FileName + ",";
                        cnt++;
                    }
                    post.Attestation = attestfiles;

                    //save file on disk
                    if (Save(cv, lettre, attest, post))
                    {
                        try
                        {
                            post.PostTime = DateTime.UtcNow;
                            mdbc.Postulerinfo.Add(post);
                            mdbc.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        catch
                        {
                            Effacer(post.CheminCv, post.Lettre, post.Attestation);
                            ViewBag.Message = "Une erreur est survenue,réessayez s'il vous plaît";
                            return View();
                        }

                    }
                    else
                    {
                        Effacer(post.CheminCv, post.Lettre, post.Attestation);
                        ViewBag.Message = "Une erreur est survenue,réessayez s'il vous plaît";
                        return View();
                    }

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

        //POST: Postuler/Demander
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Demander(int id, Postuler post, HttpPostedFileBase cv, HttpPostedFileBase lettre, HttpPostedFileBase[] attest)
        {
            try
            {
                if (cv.ContentLength > 5000000 || lettre.ContentLength > 5000000)
                {
                    ViewBag.Message = "La taille du cv ou de la lettre ne doit pas excéder 5MB";
                    return View();
                }

                if (cv != null || lettre != null || attest != null)
                {
                    //files are provided
                    //Cv
                    CreateFolderIfNotExist(Server.MapPath("~/Post/CV"));

                    post.CheminCv = "/Post/CV/" + CheckName(cv.FileName, "/Post/CV/");

                    //lettre
                    CreateFolderIfNotExist(Server.MapPath("~/Post/Lettre"));
                    post.Lettre = "/Post/Lettre/" + CheckName(lettre.FileName, "/Post/Lettre/");

                    //attestation
                    CreateFolderIfNotExist(Server.MapPath("~/Post/Attestation"));
                    string attestfiles = "";
                    int cnt = 1;
                    foreach (var item in attest)
                    {
                        if (cnt == attest.Length)
                            attestfiles += "/Post/Attestation/" + CheckName(item.FileName, "/Post/Attestation/");
                        else
                            attestfiles += "/Post/Attestation/" + CheckName(item.FileName, "/Post/Attestation/") + ",";
                        cnt++;
                    }
                    post.Attestation = attestfiles;

                    //save file on disk
                    if (Save(cv, lettre, attest, post))
                    {
                        try
                        {
                            post.RecrutementId = id;//Convert.ToInt32(recid);
                            post.PostTime = DateTime.UtcNow;
                            mdbc.Postulerinfo.Add(post);
                            mdbc.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        catch
                        {
                            Effacer(post.CheminCv, post.Lettre, post.Attestation);
                            ViewBag.Message = "Une erreur est survenue,réessayez s'il vous plaît";
                            return View();
                        }

                    }
                    else
                    {
                        Effacer(post.CheminCv, post.Lettre, post.Attestation);
                        ViewBag.Message = "Une erreur est survenue,réessayez s'il vous plaît";
                        return View();
                    }

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

        bool Save(HttpPostedFileBase filecv, HttpPostedFileBase filelettre, HttpPostedFileBase[] fileatt, Postuler postuler)
        {
            bool a, b, c;
            a = SaveImg(filecv, postuler.CheminCv);
            b = SaveImg(filelettre, postuler.Lettre);
            c = a = SaveImg(fileatt, postuler.Attestation);
            if (a && b && c)
                return true;
            else
                return false;
        }

        bool SaveImg(HttpPostedFileBase fileBase, string chemin)
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

        bool SaveImg(HttpPostedFileBase[] fileBase, string chemin)
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

        void CreateFolderIfNotExist(string dirname)
        {
            if (!Directory.Exists(dirname))
                Directory.CreateDirectory(dirname);
        }

        void DeleteOnError(string path)
        {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
        void Effacer(string cv, string lettre, string attestation)
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Postuler post = mdbc.Postulerinfo.Find(id);
            if (post == null)
                return RedirectToAction("Index");
            return View(post);
        }

        // POST: Postuler/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthUsers(AccessLevelOne = Role.Administrateur, AccessLevelTwo = Role.Cv_Administrateur)]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Postuler rec = mdbc.Postulerinfo.Find(id);
                mdbc.Postulerinfo.Remove(rec);
                mdbc.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Erreur = "Impossible de supprimer l'élément selectionné";
                return View();
            }
        }

        List<Postuler> SearchById(string id)
        {
            if (IsNumeric(id))
            {
                int k = Convert.ToInt32(id);
                var post = from recru in mdbc.Postulerinfo where recru.RecrutementId == k select recru;
                return post.ToList();
            }
            else
            {
                if (id.Contains("spontanée"))
                {
                    var post = from recru in mdbc.Postulerinfo where recru.RecrutementId == 0 select recru;
                    return post.ToList();
                }
                return null;
            }

        }
        bool IsNumeric(string str)
        {
            int k;
            return int.TryParse(str, out k);
        }
        List<Postuler> SearchByPost(string id)
        {
            var post = mdbc.Postulerinfo.Where(x => x.PostOccupe.Contains(id));
            return post.ToList();
        }
        List<Postuler> SearchByNom(string id)
        {
            var post = mdbc.Postulerinfo.Where(x => x.Nom.Contains(id));
            return post.ToList();
        }
        List<Postuler> SearchByPrenom(string id)
        {
            var post = mdbc.Postulerinfo.Where(x => x.Prenom.Contains(id));
            return post.ToList();
        }


        void Appender(List<Postuler> l0, List<Postuler> l1)
        {
            if (l1 != null)
            {
                l0.AddRange(l1);
                l1.Clear();
            }
        }
        List<Postuler> Search(string[] str)
        {
            List<Postuler> lst = new List<Postuler>();
            int tour = str.Length;
            for (int i = 0; i < tour; i++)
            {
                lst.AddRange(SearchItterator(str[i]));
            }
            return lst;
        }
        List<Postuler> SearchItterator(string str)
        {
            List<Postuler> lstIdent = new List<Postuler>();
            List<Postuler> lst = new List<Postuler>();
            lst = SearchById(str);
            Appender(lstIdent, lst);
            lst = SearchByPost(str);
            Appender(lstIdent, lst);
            lst = SearchByNom(str);
            Appender(lstIdent, lst);
            lst = SearchByPrenom(str);
            Appender(lstIdent, lst);
            return lstIdent;
        }


        List<Postuler> Resultat(string str)
        {
            Critere = str.Split(',');
            if (Critere.Length >= 1)
                return DuplicateData(Search(Critere));
            else
                return null;
        }
        List<Postuler> DuplicateData(List<Postuler> lst)
        {
            var uniqueitem = lst.Distinct(new DistinctPostulerComparer());
            return uniqueitem.ToList();
        }

        List<Postuler> GetSearchList(string str)
        {
            return Resultat(str);
        }

        string CheckName(string filename, string dir)
        {
            FileInfo fi = new FileInfo(filename);
            string fn = fi.Name;
            string p = "~" + dir + fn;
            int cnt = 0;
            string chemin = fn;
            while (System.IO.File.Exists(Server.MapPath(p)))
            {
                p = "~" + dir + cnt + "_" + filename;
                chemin = cnt + "_" + filename;
                cnt++;
            }
            return chemin;
        }
    }
}
