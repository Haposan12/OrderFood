using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PesanMakanan.Models;
namespace PesanMakanan.Controllers
{
    public class PenjualController : Controller
    {
        // GET: Penjual
        public ActionResult Index()
        {
            OrderFoodEntities db = new OrderFoodEntities();
            string userName = (string)Session["userName"];
            var toko = db.Tokoes.Where(x => x.NamaToko == userName).FirstOrDefault();

            int idToko = toko.TokoID;
            var pesanan = db.Pesanans.Where(x => x.IDToko == idToko).ToList();

            ViewBag.data = pesanan;
            return View();
        }

        
        public ActionResult Bayar(int id)
        {
            using(OrderFoodEntities db = new OrderFoodEntities())
            {
                var pesanan = db.Pesanans.Where(x => x.IDPesanan == id).FirstOrDefault();
                pesanan.Status = 1;
                db.SaveChanges();
            }
            TempData["SuccessMessage"] = "Pesanan Sudah Dibayar";

            return RedirectToAction("Index", "Penjual");
        }
    }
}