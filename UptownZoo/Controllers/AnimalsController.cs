using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UptownZoo.Models;

namespace UptownZoo.Controllers {
    public class AnimalsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Animals
        public ActionResult Index() {
            return View(AnimalService.GetAllAnimals(db));
        }

        // GET: Animals/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null) {
                return HttpNotFound();
            }
            return View(animals);
        }

        // GET: Animals/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnimalID,Name")] Animals animals) {
            if (ModelState.IsValid) {
                db.Animals.Add(animals);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(animals);
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Animals animals = db.Animals.Find(id);
            if (animals == null) {
                return HttpNotFound();
            }

            return View(animals);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnimalID,Name")] Animals animals) {
            if (ModelState.IsValid) {
                db.Entry(animals).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(animals);
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Animals animals = db.Animals.Find(id);
            if (animals == null) {
                return HttpNotFound(); 
            }

            return View(animals);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Animals animals = db.Animals.Find(id);
            db.Animals.Remove(animals);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
