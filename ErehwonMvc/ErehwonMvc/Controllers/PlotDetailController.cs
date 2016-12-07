using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErehwonMvc.ApiController;
using ErehwonMvc.Helpers;
using ErehwonMvc.Models;

namespace ErehwonMvc.Controllers
{
    public class PlotDetailController : Controller
    {
        // GET: PlotDetail/Plot
        public ActionResult Plot(int id)
        {
            var cont = new PlotCategoryController();
            var plotCategory = cont.Get(id).First();
            var plotDetail = new PlotDetailModel()
            {
                Hectares = plotCategory.TotalHectares.Value,
                PlotCategoryId = plotCategory.PlotCategoryID,
                PlotDescription = plotCategory.PlotCategoryDescription,
                PlotName = plotCategory.PlotCategoryName,
                PricePaidPerHectare = plotCategory.PricePerHectare.Value
            };

            return View(plotDetail);
        }

        public ActionResult Purchase(PlotDetailModel plotDetailModel)
        {
            var orderController = new OrderController();
            var plots = new List<Plot>
            {
                new Plot
                {
                    PlotCategoryID = plotDetailModel.PlotCategoryId,
                    PlotDescription = plotDetailModel.PlotDescription,
                    PlotName = plotDetailModel.PlotName,
                    TotalHectares = 1.0,
                    Price = plotDetailModel.PricePaidPerHectare
                }
            };
            orderController.Create(plots);

            return Redirect("/Order");
        }        
    }
}
