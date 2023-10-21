using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab_1.Controllers
{
    public class Lab1Controller : Controller
    {
        // GET: Lab1
        public ActionResult FirstViewMethod()
        {
            ViewBag.vegetables = GetVegetablesList();
            return View();
        }
        public List<string> GetVegetablesList()
        {
            List<string> list = new List<string>();
            list.Add("Томат");
            list.Add("Помидор");
            list.Add("Огурец");
            list.Add("Морковь");
            list.Add("Картофель");
            list.Add("Лук");
            list.Add("Чеснок");
            list.Add("Свекла");
            list.Add("Капуста");

            return list;
        }
        public ActionResult SecondViewMethod()
        {
            return View(GetVegetablesList().OrderBy(x => x).ToList());
        }
        public ActionResult ThirdViewMethod()
        {
            return View(GetVegetablesList().OrderBy(x => x).ToList());
        }
        public ActionResult MyViewMethod()
        {
            return View(GetVegetablesList());
        }
    }
}