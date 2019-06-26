using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PesanMakanan.Models;

namespace PesanMakanan.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(PesanMakanan.Models.User userModel)
        {
            using (OrderFoodEntities db = new OrderFoodEntities())
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && 
                x.Password == userModel.Password).FirstOrDefault();

                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;
                    Session["role"] = userDetails.Role;
                    if((int)Session["role"] == 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Penjual");
                    }
                    
                }
            }
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Register(int id=0)
        {
            User userModel = new User();
            return View();
        }

        [HttpPost]
        public ActionResult Register(User userModel)
        {
            using(OrderFoodEntities db = new OrderFoodEntities())
            {
                if(db.Users.Any(x => x.UserName == userModel.UserName))
                {
                    ViewBag.DuplicateMessage = "Username already exist";
                    return View("Register", userModel);
                }

                db.Users.Add(userModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successfull.";

            return RedirectToAction("Index","Login");
        }
    }
}