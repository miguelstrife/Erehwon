﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErehwonMvc.Models;

namespace ErehwonMvc.Controllers
{
    public class PlotAvailabilityController : Controller
    {
        // GET: PlotAvailability
        public ActionResult PlotAvailability()
        {
            return View();
        }

        public ActionResult GetPlots()
        {
            return null;
        }
    }
}