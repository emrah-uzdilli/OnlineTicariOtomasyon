using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }

            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> values = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun p)
        {
            
            c.Uruns.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Urunsil(int id)
        {
            var ktg = c.Uruns.Find(id);
            c.Uruns.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> values = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urn = c.Uruns.Find(p.UrunID);
            urn.UrunAlis = p.UrunAlis;
            urn.Durum = p.Durum;
            urn.Marka = p.Marka;
            urn.UrunSatis = p.UrunSatis;
            urn.Stok = p.Stok;
            urn.UrunAd = p.UrunAd;
            urn.UrunGorsel = p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();

            return View(degerler);

        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> values = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd+" "+x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            var deger2 = c.Uruns.Find(id);
            ViewBag.v2 = deger2.UrunID;
            ViewBag.v3 = deger2.UrunSatis;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {

            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index","Satis");
        }

    }
}