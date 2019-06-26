using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PesanMakanan.Models;

namespace PesanMakanan.Controllers
{
    public class PesananController : Controller
    {
        // GET: Pesanan
        public ActionResult Index()
        {
            OrderFoodEntities db = new OrderFoodEntities();
            string userName = (string)Session["userName"];
            var user = db.Users.Where(x => x.UserName == userName).FirstOrDefault();
            int idUser = user.UserID;

            var pesanan = (from p in db.Pesanans
                        join m in db.Makanans on p.IDMakanan equals m.IDMakanan
                        join t in db.Tokoes on p.IDToko equals t.TokoID
                        where p.IDUser == idUser
                        select p
                        ).ToList();
            
            
            ViewBag.pesanan = pesanan;
            
            return View();
        }
    }


}