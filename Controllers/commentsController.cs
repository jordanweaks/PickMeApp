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

namespace PickME.Controllers
{
    public class commentsController : Controller
    {
        private PickMEEntities db = new PickMEEntities();

        // GET: comments
        public ActionResult Index()
        {
            var comments = db.comments.Include(c => c.comp).Include(c => c.userProfile);
            return View(comments.ToList());
        }

        public ActionResult rateComment(int? id)
        {
            var userProfile = db.userProfiles.Find(User.Identity.GetUserId());
            var comment = db.comments.Find(id);
            if (!userProfile.comments1.Contains(comment))
            {
                userProfile.comments1.Add(comment);
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Home", new { id = comment.compId });
        }
        // GET: comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: comments/Create
        public ActionResult Create(int? id)
        {
        
            var comp = db.comps.Find(id);
            var comment = new comment
            {
                userId = User.Identity.GetUserId(),
                compId = id.Value
             
        };
            return View(comment);
        }

        // POST: comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.dateTime = DateTime.Now;
                comment.userId = User.Identity.GetUserId();

                db.comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details","Home", new {id=comment.compId });
            }

            comment.userId = User.Identity.GetUserId();
            comment.compId = id.Value;
            return View(comment);
        }

        // GET: comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.compId = new SelectList(db.comps, "compId", "userId", comment.compId);
            ViewBag.userId = new SelectList(db.userProfiles, "userId", "userName", comment.userId);
            return View(comment);
        }

        // POST: comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentId,userId,compId,dateTime")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.compId = new SelectList(db.comps, "compId", "userId", comment.compId);
            ViewBag.userId = new SelectList(db.userProfiles, "userId", "userName", comment.userId);
            return View(comment);
        }

        // GET: comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
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
