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
    public class StudentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Student student,HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                string filename = student.Id.ToString() + "." + img.FileName.Split('.')[1];
                img.SaveAs(Server.MapPath("~/image/"+ filename));
                student.studentimg = filename;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName", student.DeptId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName", student.DeptId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Student student, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                string filename = student.Id.ToString() + "." + img.FileName.Split('.')[1];
                img.SaveAs(Server.MapPath("~/image/" + filename));
                student.studentimg = filename;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName", student.DeptId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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

        public ActionResult AddCourse(int id)
        {
            var allcrs = db.Courses.ToList();
            var DeptCrs = db.Studentcrs.Where(p => p.Id == id).Select(p => p.Course).ToList();
            var crsnotinDept = allcrs.Except(DeptCrs).ToList();
            ViewBag.dept = db.Students.FirstOrDefault(p => p.Id == id);

            return View(crsnotinDept);
        }
        [HttpPost]
        public ActionResult AddCourse(int id, Dictionary<string, bool> crs)
        {
            //KeyValuePair<string, bool>
            foreach (var item in crs)
            {
                if (item.Value == true)
                {
                    db.Studentcrs.Add(new studentcrs() { Id = id, CrsId = int.Parse(item.Key) });
                }
            }

            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult RemoveCourse(int id)
        {

            var DeptCrs = db.Studentcrs.Where(p => p.Id == id).Select(p => p.Course).ToList();

            ViewBag.dept = db.Students.FirstOrDefault(p => p.Id == id);

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
                    var crsDelete = db.Studentcrs.FirstOrDefault(p => p.Id == id && p.CrsId == x);
                    db.Studentcrs.Remove(crsDelete);
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        //studentCourse
        public ActionResult AddDegree(int id)
        {
            var allCourses = db.Courses.ToList();
            var courseWithDegree = db.Studentcrs.Where(a => a.Id == id&&(a.Degree!=0)).Select(m => m.Course);
            var courseWithNoDegree = allCourses.Except(courseWithDegree).ToList();
            ViewBag.deg = db.Courses.FirstOrDefault(a => a.CrsId == id);
            return View(courseWithNoDegree);
        }
        [HttpPost]
        public ActionResult AddDegree(int id, Dictionary<string, int> deg)
        {
            foreach (var item in deg)
            {
                if (item.Value >0)
                {
                    db.Studentcrs.Add(new studentcrs() { Id = id, CrsId = int.Parse(item.Key),Degree=item.Value });
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }


    }
}
