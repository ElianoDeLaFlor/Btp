using Btp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class HomeController : Controller
    {
        ModelDBContext mdbc = new ModelDBContext();
        // GET: Home
        public ActionResult Index()
        {
            var lst = from img in mdbc.ImageManagerinfo select img;
            
            ViewBag.Slide = SlideList(lst.ToList());
            return View();
        }
        private String SlideList(List<ImageManager> list)
        {
            string lst = "";
            for (int i=0;i<list.Count;i++)
            {
                ImageManager item = list[i];
                lst += item.Chemin + "," + item.Title + "," + item.Description + ";";
                if(i==list.Count-1)
                    lst += item.Chemin + "," + item.Title + "," + item.Description;
            }
            return lst;
        }
        public ActionResult materiel()
        {
            return View();
        }
        
    }
}