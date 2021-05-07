using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class CourseController : Controller
    {
        private Model1 db = new Model1();

        // GET: Course
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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

        public ActionResult AddDept(int id)
        {
            var allDepts = db.Departments.ToList();
            var StdDepts = db.Departmentcrs.Where(p => p.CrsId == id).Select(p => p.Department);
            var crsnotinStd = allDepts.Except(StdDepts).ToList();
            ViewBag.depts = db.Courses.FirstOrDefault(p => p.CrsId == id);

            return View(crsnotinStd);
        }
        [HttpPost]
        public ActionResult AddDept(int id, Dictionary<string, bool> depts)
        {
            foreach (KeyValuePair<string, bool> item in depts)
            {
                if (item.Value == true)
                {
                    db.Departmentcrs.Add(new Departmentcrs() { CrsId = id, DeptId = int.Parse(item.Key) });
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult RemoveDept(int id)
        {

            var CrsDept = db.Departmentcrs.Where(p => p.CrsId == id).Select(p => p.Department).ToList();

            ViewBag.depts = db.Courses.FirstOrDefault(p => p.CrsId == id);

            return View(CrsDept);
        }
        [HttpPost]
        public ActionResult RemoveDept(int id, Dictionary<string, bool> depts)
        {
            foreach (KeyValuePair<string, bool> item in depts)
            {
                if (item.Value == true)
                {
                    int x = int.Parse(item.Key);
                    var deptDelete = db.Departmentcrs.FirstOrDefault(p => p.DeptId == id && p.CrsId == x);
                    if (deptDelete != null)
                    {
                        ViewBag.ErrorMessage = "Department ID can't set null";
                        db.Departmentcrs.Remove(deptDelete);
                        
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        
    }
}
