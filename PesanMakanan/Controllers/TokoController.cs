using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PesanMakanan.Models;

namespace PesanMakanan.Controllers
{
    public class TokoController : Controller
    {
      
        // GET: Toko
        public ActionResult Index()
        {
            OrderFoodEntities entities = new OrderFoodEntities();
            
            var data = entities.Tokoes.ToList();
            ViewBag.tokoDetail = data;
            ViewBag.user = Session["userName"];
            return View();
        }

        [HttpGet]
        public ActionResult Proses(string toko)
        {
            ViewBag.namaToko = toko;

            return View();
        }

        [HttpGet]
        public ActionResult ListJualan(string tipe, string toko)
        {
            ViewBag.tipe = tipe;
            ViewBag.toko = toko;
            OrderFoodEntities db = new OrderFoodEntities();
            var jualan = db.Makanans.Where(x => x.TipeMakanan == tipe).ToList();

            ViewBag.jualan = jualan;
            return View();
        }

        [HttpGet]
        public ActionResult Pesan(int id, string toko) 
        {
            
            ViewBag.toko = toko;
            OrderFoodEntities db = new OrderFoodEntities();
            var pesanan = db.Makanans.Where(x => x.IDMakanan == id).FirstOrDefault();
            var tokoDetail = db.Tokoes.Where(x => x.NamaToko == toko).FirstOrDefault();

            ViewBag.idToko = tokoDetail.TokoID;
            ViewBag.idMakanan = pesanan.IDMakanan;
            ViewBag.namaMakanan = pesanan.NamaMakanan;
            ViewBag.tipeMakanan = pesanan.TipeMakanan;
            ViewBag.gambar = pesanan.Gambar;
            ViewBag.harga = pesanan.Harga;

            return View();
        }

        [HttpPost]
        public ActionResult Pesan(Pesanan pesanModel)
        {
            using(OrderFoodEntities db = new OrderFoodEntities())
            {
                db.Pesanans.Add(pesanModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            TempData["SuccessMessage"] = "Pesan Berhasil";

            return RedirectToAction("Index", "Pesanan");
        }
    }
}