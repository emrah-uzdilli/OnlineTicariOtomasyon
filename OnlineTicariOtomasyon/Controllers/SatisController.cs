using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context c = new Context();

        // GET: Satis
        public ActionResult Index()
        {
            var deger = c.SatisHarekets.ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult SatisEkle()
        {
            List<SelectListItem> values1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            ViewBag.v1 = values1;
            List<SelectListItem> values2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            ViewBag.v2 = values2;
            List<SelectListItem> values3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.v3 = values3;
            return View();
        }
        [HttpPost]
        public ActionResult SatisEkle(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> values1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            ViewBag.v1 = values1;
            List<SelectListItem> values2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            ViewBag.v2 = values2;
            List<SelectListItem> values3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.v3 = values3;
            var urundeger = c.SatisHarekets.Find(id);
            return View("SatisGetir", urundeger);
        }
        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var sat = c.SatisHarekets.Find(s.SatisID);
            sat.UrunID = s.UrunID;
            sat.CariID = s.CariID;
            sat.PersonelID = s.PersonelID;
            sat.PersonelID = s.PersonelID;
            sat.Adet = s.Adet;
            sat.Fiyat = s.Fiyat;
            sat.ToplamTutar = s.ToplamTutar;
            sat.Tarih = s.Tarih;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(degerler);
        }
    }
}