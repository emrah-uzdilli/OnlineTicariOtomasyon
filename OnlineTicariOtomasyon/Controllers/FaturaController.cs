using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context c = new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var urundeger = c.Faturalars.Find(id);
            return View("FaturaGetir", urundeger);
        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fat = c.Faturalars.Find(f.FaturaID);
            fat.FaturaSeriNo = f.FaturaSeriNo;
            fat.FaturaSıraNo = f.FaturaSıraNo;
            fat.VergiDairesi = f.VergiDairesi;
            fat.Tarih = f.Tarih;
            fat.Saat = f.Saat;
            fat.TeslimAlan = f.TeslimAlan;
            fat.TeslimEden = f.TeslimEden;
            fat.Toplam = f.Toplam;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.FaturaID == id).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalemEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalemEkle(FaturaKalem f)
        {
            c.FaturaKalems.Add(f);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Dinamik()
        {
            DinamikFatura cs = new DinamikFatura();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();

            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo,string FaturaSıraNo,DateTime Tarih,
            string VergiDairesi,string Saat,string TeslimAlan,string TeslimEden,string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimAlan = TeslimAlan;
            f.TeslimEden = TeslimEden;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach(var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Acıklama = x.Acıklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.FaturaID = x.FaturaKalemID;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalems.Add(fk);
            }

            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}


