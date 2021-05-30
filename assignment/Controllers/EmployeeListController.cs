using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using assignment.Models;

namespace assignment.Controllers
{
    public class EmployeeListController : Controller
    {
        private DBModel db = new DBModel();

        // GET: EmployeeList
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
                List<employee_detail> emplist = db.employee_details.ToList<employee_detail>();
                return Json(new { data = emplist }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {

            if (id == 0)
                return View(new employee_detail());
            else
            {
                return View(db.employee_details.Where(x => x.Id == id).FirstOrDefault<employee_detail>());
            }
      
        }

        [HttpPost]
        public ActionResult AddOrEdit(employee_detail emp)
        { 

            if(emp.Id == 0) { 
                db.employee_details.Add(emp);
                db.SaveChanges();
                Json(new { success = true, message = "save successfully" }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            else
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                Json(new { success = true, message = "Updated successfully" }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            employee_detail emp = db.employee_details.Where(x => x.Id == id).FirstOrDefault<employee_detail>();
            db.employee_details.Remove(emp);
            db.SaveChanges();
            return Json(new { success = true, message = "Delete successfully" }, JsonRequestBehavior.AllowGet);
            
        }

    }
}