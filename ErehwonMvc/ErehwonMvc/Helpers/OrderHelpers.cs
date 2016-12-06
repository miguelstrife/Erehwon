using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using ErehwonMvc.Models;

namespace ErehwonMvc.Helpers
{
    public static class OrderHelpers
    {
        private const string CookieName = "Erehwon";

        private static readonly ErehwonDataContext DataContext = new ErehwonDataContext();

        public static void AddItem(Plot plot)
        {
            var tempOrderId = GetTempOrderId();
            var order = DataContext.Orders.FirstOrDefault(x => tempOrderId == x.Guid.ToString());
            if (order != null)
            {
                order.Plots.Add(plot);
            }
            else
            {
                var plotList = new EntitySet<Plot> {plot};
                order = new Order()
                {
                    Guid = new Guid(tempOrderId),
                    Plots = plotList
                };
                DataContext.Orders.InsertOnSubmit(order);

                try
                {
                    DataContext.SubmitChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static void RemoveItem(Plot plot)
        {
            DataContext.Plots.DeleteOnSubmit(plot);

            try
            {
                DataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string GetTempOrderId()
        {
            //Check if we have an existent order

            var cookie = HttpContext.Current.Response.Cookies.Get(CookieName);

            if (!string.IsNullOrWhiteSpace(cookie?.Value)) return cookie.Value;

            // If we dont find one, generate a new id
            var tempOrderId = Guid.NewGuid();
            var order = new HttpCookie(CookieName, tempOrderId.ToString())
            { 
                Expires = DateTime.Now.AddDays(10)
            };
            HttpContext.Current.Response.Cookies.Add(order);
            return order.Value;
        }

        public static Order GetOrderByOrderId(int orderId)
        {
            return DataContext.Orders.FirstOrDefault(x => x.OrderID == orderId);
        }

        public static Order GetOrderByGuid(Guid guid)
        {
            return DataContext.Orders.FirstOrDefault(x => x.Guid == guid);
        }

        public static Order CreateOrder(Order order)
        {
            if (order.OrderID != 0)
            {
                throw new ArgumentException("Order already exists");
            }
            DataContext.Orders.InsertOnSubmit(order);
             
            try
            {
                DataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return DataContext.Orders.FirstOrDefault(x => x.Guid == order.Guid);
        }

    }
}