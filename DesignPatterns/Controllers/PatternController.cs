using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DesignPatterns.Models;

namespace DesignPatterns.Controllers
{
    public class PatternController : Controller
    {
        private DesignPatternsEntities db = new DesignPatternsEntities();

        // GET: Pattern
        public ActionResult Index()
        {
            var patterns = db.Patterns.Include(p => p.PatternType);
            return View(patterns.ToList());
        }

        // GET: Pattern/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pattern pattern = db.Patterns.Find(id);
            if (pattern == null)
            {
                return HttpNotFound();
            }
            return View(pattern);
        }

        // GET: Pattern/Create
        public ActionResult Create()
        {
            ViewBag.PatternTypeId = new SelectList(db.PatternTypes, "Id", "Name");
            return View();
        }

        // POST: Pattern/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,NameTranslated,Description,PatternTypeId,IsActive")] Pattern pattern)
        {
            if (ModelState.IsValid)
            {
                pattern.Id = Guid.NewGuid();
                db.Patterns.Add(pattern);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatternTypeId = new SelectList(db.PatternTypes, "Id", "Name", pattern.PatternTypeId);
            return View(pattern);
        }

        // GET: Pattern/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pattern pattern = db.Patterns.Find(id);
            if (pattern == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatternTypeId = new SelectList(db.PatternTypes, "Id", "Name", pattern.PatternTypeId);
            return View(pattern);
        }

        // POST: Pattern/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,NameTranslated,Description,PatternTypeId,IsActive")] Pattern pattern)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pattern).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatternTypeId = new SelectList(db.PatternTypes, "Id", "Name", pattern.PatternTypeId);
            return View(pattern);
        }

        // GET: Pattern/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pattern pattern = db.Patterns.Find(id);
            if (pattern == null)
            {
                return HttpNotFound();
            }
            return View(pattern);
        }

        // POST: Pattern/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Pattern pattern = db.Patterns.Find(id);
            db.Patterns.Remove(pattern);
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
