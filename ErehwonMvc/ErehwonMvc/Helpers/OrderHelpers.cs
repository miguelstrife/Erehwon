using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using ErehwonMvc.Models;
using WebGrease.Css.Extensions;

namespace ErehwonMvc.Helpers
{
    public class OrderHelpers
    {
        private const string CookieName = "Erehwon";

        private  readonly ErehwonDataContext DataContext = new ErehwonDataContext();

        public  void AddItem(Plot plot)
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
            }
           
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

        public  void RemoveItem(Plot plot)
        {
            var plotToDelete = DataContext.Plots.First(x => x.PlotID == plot.PlotID);
            DataContext.Plots.DeleteOnSubmit(plotToDelete);

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

        public  string GetTempOrderId()
        {
            //Check if we have an existent order

            var cookie = HttpContext.Current.Request.Cookies.Get(CookieName);

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

        public  Order GetOrderByOrderId(int orderId)
        {
            return DataContext.Orders.FirstOrDefault(x => x.OrderID == orderId);
        }

        public  Order GetOrderByGuid(Guid guid)
        {
            return DataContext.Orders.FirstOrDefault(x => x.Guid == guid);
        }

        public  Order CreateOrder(Order order)
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

        public  void RemoveOrderGuid()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(CookieName);

            if (string.IsNullOrWhiteSpace(cookie?.Value))
            {
                HttpContext.Current.Response.Cookies.Remove(CookieName);
            }
        }

        public  void UpdateOrderPlot(Plot plot)
        {
            var dbPlot = DataContext.Plots.FirstOrDefault(x => x.PlotID == plot.PlotID);
            
            if (dbPlot == null) throw new ArgumentException("Plot not Found");
            var pricePerHa = GetPlotPrice(dbPlot.PlotCategoryID);
            dbPlot.TotalHectares = plot.TotalHectares;
            dbPlot.Price = plot.TotalHectares * pricePerHa;

            DataContext.SubmitChanges();

        }

        public  double GetPlotPrice(int plotCategoryId)
        {
            return DataContext.PlotCategories.First(x => x.PlotCategoryID == plotCategoryId).PricePerHectare.Value;
        }

        public  Dictionary<int, double> GetPlotAvailability()
        {
            var categories = DataContext.PlotCategories.Select(x => x.PlotCategoryID);

            var dictionary = new Dictionary<int, double>();

            categories.ForEach(x => dictionary.Add(x, 0));

            var purchasedPlots = DataContext.Orders.Where(x => x.DateOfCompletion != null)
                .Join(DataContext.Plots, order => order.OrderID, plot => plot.OrderID,
                    (order, plot) => new {plot.PlotCategoryID, plot.TotalHectares});

            foreach (var purchasedPlot in purchasedPlots)
            {
                dictionary[purchasedPlot.PlotCategoryID] += purchasedPlot.TotalHectares;
            }

            return dictionary;
        }

        public  double GetPlotAvailableHaByPlotCategoryId(int plotCategoryId)
        {
            var result = 0.0;

            var purchasedPlots = DataContext.Orders.Where(x => x.DateOfCompletion != null)
                .Join(DataContext.Plots, order => order.OrderID, plot => plot.OrderID,
                    (order, plot) => new { plot.PlotCategoryID, plot.TotalHectares })
                    .Where(x => x.PlotCategoryID == plotCategoryId);

            foreach (var purchasedPlot in purchasedPlots)
            {
                result += purchasedPlot.TotalHectares;
            }

            var plotCategory = DataContext.PlotCategories.First(x => x.PlotCategoryID == plotCategoryId);

            return plotCategory.TotalHectares.Value - result;
        }

    }
}