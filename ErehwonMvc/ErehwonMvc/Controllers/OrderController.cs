using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using ErehwonMvc.Helpers;
using ErehwonMvc.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace ErehwonMvc.Controllers
{
    public class OrderController : Controller
    {
        private OrderHelpers OrderHelpers = new OrderHelpers();
        private AccountHelper AccountHelper = new AccountHelper();
        private PurchaseHelpers PurchaseHelpers = new PurchaseHelpers();

        // GET: Order
        public ActionResult Index()
        {
            

            var orderId = OrderHelpers.GetTempOrderId();
            var currentOrder = OrderHelpers.GetOrderByGuid(new Guid(orderId));

            // TODO: find a better way to check if Auth User has a pending order
            if (currentOrder == null)
            {
                currentOrder = new Order()
                {
                    Plots = new EntitySet<Plot>()
                };
            }
            // Experimenting with ViewData instead of using a ViewModel
            ViewData["MyOrderPlotList"] = currentOrder.Plots.ToList();

            if (currentOrder.ClientID == null && User.Identity.IsAuthenticated)
            {
                var clientId = AccountHelper.GetClientIdByUserId(User.Identity.GetUserId());
                AccountHelper.TieAccountOrders(clientId);
            }

            return View(currentOrder);
        }

        [HttpGet]
        public string Get(int id)
        {
            var order = OrderHelpers.GetOrderByOrderId(id);
            return JsonConvert.SerializeObject(order);
        }

        [HttpPost]
        public int Create(List<Plot> plots)
        {
            var tempOrderId = OrderHelpers.GetTempOrderId();

            plots.ForEach(OrderHelpers.AddItem);

            return OrderHelpers.GetOrderByGuid(new Guid(tempOrderId)).OrderID;
        }

        public ActionResult Remove(Plot plot)
        {
            OrderHelpers.RemoveItem(plot);
            return Redirect("/Order");
        }

        [HttpPost]
        public ActionResult UpdatePlot(Plot plot)
        {
            OrderHelpers.UpdateOrderPlot(plot);
            return Redirect("/Order");
        }

        [Authorize]
        public ActionResult Finalize(string orderId)
        {
            var validate = PurchaseHelpers.ValidateOrder(orderId);
            if (!validate)
            {
                // Sth
            }
            var purchaseResult = PurchaseHelpers.FinalizePurchase(orderId);
            OrderHelpers.RemoveOrderGuid();

            return purchaseResult ? View("PurchaseComplete") : View("Error");
        }
    
    }
}