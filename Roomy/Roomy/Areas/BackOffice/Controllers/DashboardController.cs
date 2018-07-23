﻿using Roomy.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roomy.Areas.BackOffice.Controllers
{
    [AuthenticationFilter]

    public class DashboardController : Controller
    {
        // GET: BackOffice/Dashboard

        public ActionResult Index()
        {
            return View();
        }
    }
}