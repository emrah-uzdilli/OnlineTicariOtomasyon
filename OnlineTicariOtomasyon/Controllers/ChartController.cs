using OnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index3()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult()
        {
            return Json(UrunList(), JsonRequestBehavior.AllowGet);
        }

        public List<Chart> UrunList()
        {
            List<Chart> chrt = new List<Chart>();

            using (var c = new Context())
            {
                chrt = c.Uruns.Select(x => new Chart
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }
            return chrt;
        }
    }
}