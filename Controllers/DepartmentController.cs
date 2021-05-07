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
    public class DepartmentController : Controller
    {
        private Model1 db = new Model1();

        // GET: Department
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        public ActionResult AddCourse(int id)
        {
            var allcrs = db.Courses.ToList();
            var DeptCrs = db.Departmentcrs.Where(p => p.DeptId == id).Select(p => p.Course);
            var crsnotinDept = allcrs.Except(DeptCrs).ToList();
            ViewBag.dept = db.Departments.FirstOrDefault(p => p.DeptId == id);

            return View(crsnotinDept);
        }
        [HttpPost]
        public ActionResult AddCourse(int id,Dictionary<string,bool> crs)
        {
            foreach (KeyValuePair<string,bool> item in crs)
            {
                if (item.Value==true)
                {
                    db.Departmentcrs.Add(new Departmentcrs() { DeptId = id, CrsId = int.Parse(item.Key) });
                }
            }

            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult RemoveCourse(int id)
        {
           
            var DeptCrs = db.Departmentcrs.Where(p => p.DeptId == id).Select(p => p.Course).ToList();
            
            ViewBag.dept = db.Departments.FirstOrDefault(p => p.DeptId == id);

            return View(DeptCrs);
        }
        [HttpPost]
        public ActionResult RemoveCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (KeyValuePair<string, bool> item in crs)
            {
                if (item.Value == true)
                {
                    int x = int.Parse(item.Key);
                    var crsDelete = db.Departmentcrs.FirstOrDefault(p => p.DeptId == id && p.CrsId == x);
                    db.Departmentcrs.Remove(crsDelete);
                }
            }

            db.SaveChanges();
            return RedirectToAction("index");
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
