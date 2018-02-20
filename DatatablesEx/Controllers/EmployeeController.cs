using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatatablesEx.Models;
using System.Data.Entity;

namespace DatatablesEx.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using(EmployeesEntities db = new EmployeesEntities())
            {
                List<Employee> empList = db.Employees.ToList<Employee>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            using (EmployeesEntities db = new EmployeesEntities())
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            using (EmployeesEntities db = new EmployeesEntities())
                {
                    return View(db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>());
                }
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            using (EmployeesEntities db = new EmployeesEntities())
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (EmployeesEntities db = new EmployeesEntities())
            {
                Employee emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}