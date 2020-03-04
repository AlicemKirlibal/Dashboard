using Dashboard.DB;
using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class HomeController : Controller

    {
        private Tempo_MillenicomSalesDashboardEntities context = new Tempo_MillenicomSalesDashboardEntities();

        // GET: Home
        public ActionResult Index()
        {

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            DateTime date = Convert.ToDateTime(dateTime);
            date.ToString("yyyy-MM-DD");
            var result = context.Users.OrderByDescending(i => i.SalesCount).Where(e => e.SalesDate == date).ToList();

            int salesTotalCount = 0;
            foreach (var item in result.Select(e => e.SalesCount))
            {
                salesTotalCount = salesTotalCount + item;
            }
            ViewBag.SalesCounts = salesTotalCount;


            return View(result);
        }


        public ActionResult satisAdetAzalt(int id)
        {
            Users eleman = context.Users.Where(i => i.Id == id).FirstOrDefault();

            if (eleman.SalesCount != 0)
            {
                eleman.SalesCount--;
            }
            else
            {
                context.Users.Remove(eleman);
            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}