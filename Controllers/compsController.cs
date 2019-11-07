using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PickME.Models;
using PickME.Models.viewModels;

namespace PickME.Controllers
{
    public class compsController : Controller
    {

        private PickMEEntities db = new PickMEEntities();
        private footballAPI api = new footballAPI();

        // GET: comps
        public ActionResult Index()
        {
            var comps = db.comps.Include(c => c.userProfile).Where(c=>c.exp>DateTime.Now).AsEnumerable().Select(c=> new compViewModel(c,api));
            return View(comps);
        }

        public ActionResult FirstPlayerScore(int? id)
        {
            var userId = User.Identity.GetUserId();

            if (!db.scores.Any(s=>s.userId==userId&&s.compId==id))
            {
                var score = new score
                {
                    compId = id.Value,
                    isFirstPlayerBetter = true,
                    userId = userId
                };
                db.scores.Add(score);
                db.SaveChanges();
            }
     
            return RedirectToAction("Details", "Home", new { id = id });
        }
        public ActionResult SecondPlayerScore(int? id)
        {
            var userId = User.Identity.GetUserId();

            if (!db.scores.Any(s => s.userId == userId && s.compId == id))
            {
                var score = new score
                {
                    compId = id.Value,
                    isFirstPlayerBetter = false,
                    userId = userId
                };
                db.scores.Add(score);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Home", new { id = id });

        }




        // GET: comps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comp comp = db.comps.Find(id);
            if (comp == null)
            {
                return HttpNotFound();
            }
            return View(comp);
        }

        // GET: comps/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.userProfiles, "userId", "userName");
            return View();
        }

        // POST: comps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "compId,userId,firstPlayerId,secondPlayerId,exp")] comp comp)
        {
            if (ModelState.IsValid)
            {
                db.comps.Add(comp);
                db.SaveChanges();
                return RedirectToAction("Account", "Home");
            }

            ViewBag.userId = new SelectList(db.userProfiles, "userId", "userName", comp.userId);
            return View(comp);
        }

        // GET: comps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comp comp = db.comps.Find(id);
            if (comp == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.userProfiles, "userId", "userName", comp.userId);
            return View(comp);
        }

        // POST: comps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "compId,userId,firstPlayerId,secondPlayerId,exp")] comp comp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.userProfiles, "userId", "userName", comp.userId);
            return View(comp);
        }

        // GET: comps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comp comp = db.comps.Find(id);
            if (comp == null)
            {
                return HttpNotFound();
            }
            return View(comp);
        }

        // POST: comps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comp comp = db.comps.Find(id);
            db.comps.Remove(comp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
