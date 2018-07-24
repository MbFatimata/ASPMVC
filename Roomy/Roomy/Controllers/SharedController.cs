using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roomy.Controllers
{
    public class SharedController : BaseController
    {
        // GET: Shared
        [ChildActionOnly] //pour que l'action ne soit pas appelé via l'url
        public ActionResult TopFive()
        {
            var rooms = db.Rooms.OrderByDescending(x => x.Price).Take(5);
            return View("_Topfive", rooms);
        }
    }
}