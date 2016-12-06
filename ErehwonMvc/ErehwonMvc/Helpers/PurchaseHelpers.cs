using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using ErehwonMvc.Models;

namespace ErehwonMvc.Helpers
{
    public static class PurchaseHelpers
    {
        const double MinHect = 20.0;
        const double MaxHect = 500.0;

        public static bool ValidateOrder(OrderModel order, int clientId)
        {
            // No more than 25% of the total block or more than 500 hect.
            // Min 20 hect per plot.

            // Check if customer has a previous purchase
            var clientPurchases = GetClientPurchases(clientId);

            // Get block info
            foreach (var plotDetailModel in order.Plots)
            {
                // Min Rule
                if (plotDetailModel.Hectares < MinHect) return false;

                var maxPlotHectare = GetMaxPlotHectare(plotDetailModel.PlotCategoryId);
                var purchasedHectares = GetPurchasedHectares(plotDetailModel.PlotCategoryId);
                // Get Available Hectares
                var maxAvailableHectares = maxPlotHectare - purchasedHectares;

                // Max Rule
                if (plotDetailModel.Hectares > maxAvailableHectares) return false;
                // Prev Purchase rule
                var totalClientHectares = clientPurchases.Where(
                    x => x.Plot.PlotCategoryID == plotDetailModel.PlotCategoryId)
                    .Sum(x => x.Plot.TotalHectares) + plotDetailModel.Hectares;

                if (maxPlotHectare < MaxHect || (maxPlotHectare/4.0 < totalClientHectares)) return false;
            }

            return true;
        }

        private static double GetPurchasedHectares(int plotCategoryId)
        {
            var dc = new ErehwonDataContext();
            return dc.Plots.Where(x => x.PlotCategoryID == plotCategoryId).Sum(x => x.TotalHectares);
        }

        private static List<Purchase> GetClientPurchases(int clientId)
        {
            var dc = new ErehwonDataContext();
            var result = new List<Purchase>();
            var clientOrders = dc.Orders.Where(x => x.ClientID == clientId);
            if (clientOrders.Any())
            {
                foreach (var clientOrder in clientOrders)
                {
                    //var list = clientOrder.Purchases.Select(x => new Purchase {AmountPaid = x.AmountPaid, Order = x.Order, OrderID = x.OrderID, Plot = x.Plot, PlotID = x.PlotID, PurchaseID = x.PurchaseID}).ToList();
                    result.AddRange(clientOrder.Purchases.ToList());
                }
            }

            return result;
        }

        private static double GetMaxPlotHectare(int plotCategoryId)
        {
            var dc = new ErehwonDataContext();
            var query = dc.PlotCategories.FirstOrDefault(x => x.PlotCategoryID == plotCategoryId);
            if (query?.TotalHectares != null) return query.TotalHectares.Value;
            return 0.0;
        }
    }
}