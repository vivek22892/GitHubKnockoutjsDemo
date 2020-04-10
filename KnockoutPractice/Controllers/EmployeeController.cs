using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockoutPractice.Models;
using Newtonsoft.Json;

namespace KnockoutPractice.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDetails()
        {
            List<Employee> employee = Employee.GetEmployee();
            return Json(employee.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEmployee(Employee emp)
        {
            Employee.CreateEmployee(emp);
            return Json(emp);
        }

        [HttpPut]
        public JsonResult EditEmployee(Employee emp)
        {
            Employee.CreateEmployee(emp);
            return Json(emp);
        }

        [HttpDelete]
        public void DeleteEmployee(int id)
        {
            Employee.DeleteEmployee(id);
        }
    }
}
