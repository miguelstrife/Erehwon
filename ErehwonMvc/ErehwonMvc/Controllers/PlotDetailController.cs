using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErehwonMvc.Models;

namespace ErehwonMvc.Controllers
{
    public class PlotDetailController : Controller
    {
        private static List<PlotDetailModel> _plots = new List<PlotDetailModel>();
        // GET: PlotDetail/Plot
        public ActionResult Plot(PlotDetailModel plotDetail)
        {
            return View(plotDetail);
        }

        //// GET: PlotDetail/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PlotDetail/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PlotDetail/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PlotDetail/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PlotDetail/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PlotDetail/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PlotDetail/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
