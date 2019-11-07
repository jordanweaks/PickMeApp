using Microsoft.AspNet.Identity;
using PickME.Models;
using PickME.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PickME.Controllers
{

    //2197bf28fe4c485d8a2a0a53c0d45b10

    public class HomeController : Controller
    {
        private footballAPI api = new footballAPI();
       private PickMEEntities db = new PickMEEntities();


        public ActionResult Index()
        {
            return View();
        }
    

        //public ActionResult SearchPlayers()
        //{

        //    return View();
        //}
        //[/*Authorize*/]
        public ActionResult Account(string id)
        {

            var userId = User.Identity.GetUserId();
            var comps = db.comps.Where(c => c.exp > DateTime.Now);
            if(id == "MyComparisons")
            {
                comps = comps.Where(c => c.userId == userId);
            }
                var compVm= comps.AsEnumerable().Select(c => new compViewModel(c, api));
            return View(compVm);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Details(int? id)
        {
            var model = db.comps.Find(id);
            var vm = new compViewModel(model, api);
            return View(vm);
        }
        public ActionResult DetailsPartial(int? id)
        {
            var model = db.comps.Find(id);
            var vm = new compViewModel(model, api);
            return PartialView("Details", vm);
        }
    }
}