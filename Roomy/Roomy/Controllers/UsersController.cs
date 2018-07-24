using Roomy.Data;
using Roomy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roomy.Utils;

namespace Roomy.Controllers
{
    public class UsersController : BaseController
    {
        //private RoomyDbContext db = new RoomyDbContext(); dans BaseController
        // GET: Users
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Civilities = db.Civilities.ToList(); //dynamique
            return View();
        }

        // Post: Users
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                user.Password = user.Password.HashMD5();

                db.Users.Add(user);
                db.SaveChanges();

                TempData["Message"] = $"Utilisateur {user.Lastname} enregistré.";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Civilities = db.Civilities.ToList(); 
            return View();
        }

        //protected override void Dispose(bool disposing) //pour liberer connexion à la base de donnees lorque controleur a fini de l'utiliser 
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //} Dans BaseController
    }
}