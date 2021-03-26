using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        Context c = new Context();
        // GET: Kargo
        public ActionResult Index(string p)
        {
            var takiparama = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            {
                takiparama = takiparama.Where(y => y.TakipKodu.Contains(p));
            }

            return View(takiparama.ToList());
        }
        [HttpGet]
        public ActionResult YeniKargoEkle(string p)
        {
            Random rnd = new Random();
            string[] karekterler = { "A", "B", "C", "D", "E", "G" };
            int k1, k2, k3;
            k1 = rnd.Next(0, 6);
            k2 = rnd.Next(0, 6);
            k3 = rnd.Next(0, 6);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);

            string kod = s1.ToString() + karekterler[k1] + s2.ToString() + karekterler[k2] + s3 + karekterler[k3];
            ViewBag.takipkod = kod;
            return View();
          
        }
        [HttpPost]
        public ActionResult YeniKargoEkle(KargoDetay k)
        {
            c.KargoDetays.Add(k);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            
            return View(degerler);
        }
    }
}