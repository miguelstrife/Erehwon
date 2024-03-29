﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Transactions;
using System.Web;
using ErehwonMvc.Models;

namespace ErehwonMvc.Helpers
{
    public  class PurchaseHelpers
    {
        const double MinHect = 20.0;
        const double MaxHect = 500.0;
        private OrderHelpers OrderHelpers = new OrderHelpers();

        private  readonly ErehwonDataContext DataContext = new ErehwonDataContext();
        public  bool FinalizePurchase(string orderId)
        {
            var result = false;
            // Begin transaction
            using (var transaction = new TransactionScope())
            {
                try
                {
                    var dbOrder = DataContext.Orders.FirstOrDefault(x => new Guid(orderId) == x.Guid);
                    if (dbOrder == null)
                        throw new ArgumentException("Order Not Found");
                    dbOrder.DateOfCompletion = DateTime.Now;
                    DataContext.SubmitChanges();
                    result = true;
                    transaction.Complete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return result;
        }

        public  string ValidateOrder(string orderId)
        {
            // No more than 25% of the total block or more than 500 hect.
            // Min 20 hect per plot.

            // Get Order From DB
            var order = OrderHelpers.GetOrderByGuid(new Guid(orderId));

            if(order.ClientID == null || order.ClientID.Value == 0) throw new ArgumentException("Invalid Client");

            // Check if customer has a previous purchase
            var clientPurchases = GetClientPurchases(order.ClientID.Value);

            // Get block info
            foreach (var plotDetailModel in order.Plots)
            {
                // Min Rule
                if (plotDetailModel.TotalHectares < MinHect) return "Your plot size is below 20 ha.";

                var maxPlotHectare = GetMaxPlotHectare(plotDetailModel.PlotCategoryID);
                var freeHectares = OrderHelpers.GetPlotAvailableHaByPlotCategoryId(plotDetailModel.PlotCategoryID);

                // Max Rule
                if (plotDetailModel.TotalHectares > freeHectares) return "Not enough ha left to purchase";
                // Prev Purchase rule
                var totalClientHectares = clientPurchases.Where(
                    x => x.PlotCategoryID == plotDetailModel.PlotCategoryID)
                    .Sum(x => x.TotalHectares) + plotDetailModel.TotalHectares;

                if (totalClientHectares > MaxHect || (maxPlotHectare / 4.0 < totalClientHectares)) return "You have purchased "+totalClientHectares+" already. The maximum is "+MaxHect+" or 25% per plot";
            }

            return "";
        }

        private  List<Plot> GetClientPurchases(int clientId)
        {
            var result = new List<Plot>();
            var clientOrders = DataContext.Orders.Where(x => x.ClientID == clientId && x.DateOfCompletion != null);
            if (clientOrders.Any())
            {
                foreach (var clientOrder in clientOrders)
                {
                    //var list = clientOrder.Purchases.Select(x => new Purchase {AmountPaid = x.AmountPaid, Order = x.Order, OrderID = x.OrderID, Plot = x.Plot, PlotID = x.PlotID, PurchaseID = x.PurchaseID}).ToList();
                    result.AddRange(clientOrder.Plots.ToList());
                }
            }

            return result;
        }

        private  double GetMaxPlotHectare(int plotCategoryId)
        {
            var query = DataContext.PlotCategories.FirstOrDefault(x => x.PlotCategoryID == plotCategoryId);
            if (query?.TotalHectares != null) return query.TotalHectares.Value;
            return 0.0;
        }
    }
}