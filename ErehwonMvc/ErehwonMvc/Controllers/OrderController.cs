using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ErehwonMvc.Helpers;
using ErehwonMvc.Models;
using Newtonsoft.Json;

namespace ErehwonMvc.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Order()
        {
            return View();
        }

        public string Get(int id)
        {
            var order = OrderHelpers.GetOrderByOrderId(id);
            return JsonConvert.SerializeObject(order);
        }

        [System.Web.Mvc.HttpPost]
        public int Create(List<Plot> plots)
        {
            var tempOrderId = OrderHelpers.GetTempOrderId();
            
            var order = new Order()
            {
                Guid = new Guid(tempOrderId),
            };

            var insertedOrder = OrderHelpers.CreateOrder(order);
            plots.ForEach(OrderHelpers.AddItem);

            return insertedOrder.OrderID;
        }
    
    }
}