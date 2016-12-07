using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using ErehwonMvc.Helpers;
using ErehwonMvc.Models;
using Newtonsoft.Json;

namespace ErehwonMvc.Controllers
{
    public class OrderController : Controller
    {
        
        // GET: Order
        public ActionResult Index()
        {
            var orderId = OrderHelpers.GetTempOrderId();
            var currentOrder = OrderHelpers.GetOrderByGuid(new Guid(orderId));
            ViewData["MyOrderPlotList"] = currentOrder.Plots.ToList();
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
            // Check if we have a current order
            //var currentOrder = OrderHelpers.GetOrderByGuid(new Guid(tempOrderId)) ?? new Order()
            //{
            //    Guid = new Guid(tempOrderId),
            //};

            //var insertedOrder = OrderHelpers.CreateOrder(currentOrder);
            plots.ForEach(OrderHelpers.AddItem);

            return OrderHelpers.GetOrderByGuid(new Guid(tempOrderId)).OrderID;
        }

        public void Remove(Plot plot)
        {
            OrderHelpers.RemoveItem(plot);
        }
    
    }
}