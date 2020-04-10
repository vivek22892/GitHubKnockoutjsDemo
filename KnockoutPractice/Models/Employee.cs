using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace KnockoutPractice.Models
{
    public class Employee
    {
        public int EmpNo { get; set; }

        public string EmpName { get; set; }

        public double Salary { get; set; }

        public string DeptName { get; set; }

        public string Designation { get; set; }


        public static List<Employee> GetEmployee()
        {

            List<Employee> lstProject = new List<Employee>();
            DataSet ds = new DataSet();
            ds.ReadXml(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
            DataView dvPrograms;
            dvPrograms = ds.Tables[0].DefaultView;
            dvPrograms.Sort = "EmpNo asc";
            foreach (DataRowView dr in dvPrograms)
            {
                Employee model = new Employee();
                model.EmpNo = Convert.ToInt32(dr[0]);
                model.EmpName = Convert.ToString(dr[1]);
                model.Salary = Convert.ToInt32(dr[2]);
                model.DeptName = Convert.ToString(dr[3]);
                model.Designation = Convert.ToString(dr[4]);
                lstProject.Add(model);
            }
            return lstProject;
        }

        public static Employee CreateEmployee(Employee e)
        {
            if (e.EmpNo > 0)
            {
                XDocument xmlDoc = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
                var items = (from item in xmlDoc.Descendants("Employee") select item).ToList();
                XElement selected = items.Where(p => p.Element("EmpNo").Value == e.EmpNo.ToString()).FirstOrDefault();
                if (selected != null)
                {
                    selected.Remove();
                    xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
                }
                xmlDoc.Element("Employees").Add(new XElement("Employee", new XElement("EmpNo", e.EmpNo), new XElement("EmpName", e.EmpName), new XElement("Salary", e.Salary), new XElement("DeptName", e.DeptName), new XElement("Designation", e.Designation)));
                xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
                return e;
            }
            return e;
        }

        public static void UpdateEmployee(Employee e)
        {
            XDocument xmlDoc = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
            var items = (from item in xmlDoc.Descendants("Employee") select item).ToList();
            XElement selected = items.Where(p => p.Element("EmpNo").Value == e.EmpNo.ToString()).FirstOrDefault();
            if (selected != null)
            {
                selected.Remove();
                xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
            }
            xmlDoc.Element("Employees").Add(new XElement("Employee", new XElement("EmpNo", e.EmpNo), new XElement("EmpName", e.EmpName), new XElement("Salary", e.Salary), new XElement("DeptName", e.DeptName), new XElement("Designation", e.Designation)));
            xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
        }

        public static void DeleteEmployee(int Id)
        {

            XDocument xmlDoc = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
            var items = (from item in xmlDoc.Descendants("Employee") select item).ToList();
            XElement selected = items.Where(p => p.Element("EmpNo").Value == Id.ToString()).FirstOrDefault();
            selected.Remove();
            xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/XML/Employee.xml"));
        }
    }
}