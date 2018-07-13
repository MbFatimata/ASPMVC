using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roomy.Controllers
{
    public class TestsController : Controller
    {
        // GET: Tests
        public ActionResult Index()
        {
            //EmptyResult retourn un 200 vide;
            //FileResult retourn un 200 avec un download;
            //HttpStatusCodeResult 
            //JsonResult 200 avec json return Json("");
            //RedirectResult return Redirect();
            //ViewResult 
            return View();
        }
    }
}