using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErehwonMvc.Models;

namespace ErehwonMvc.Helpers
{
    public  class AccountHelper
    {
        private  readonly ErehwonDataContext DataContext = new ErehwonDataContext();
        private OrderHelpers OrderHelpers = new OrderHelpers();

        public  int TieAccountClient(string email, string firstName, string lastName, DateTime dateOfBirth)
        {
            var userData = DataContext.AspNetUsers.FirstOrDefault(x => x.Email == email);
            var client = new Client()
            {
                UserID = userData.Id,
                DateOfBirth = dateOfBirth,
                FirstName = firstName,
                LastName = lastName
            };
            DataContext.Clients.InsertOnSubmit(client);

            DataContext.SubmitChanges();

            var dbClient = DataContext.Clients.First(x => x.UserID == client.UserID);

            return dbClient.ClientID;
        }

        public  void TieAccountOrders(int clientId)
        {
            //Check if we have a pending order to tie it with our client
            var guid = OrderHelpers.GetTempOrderId();
            var order = DataContext.Orders.FirstOrDefault(x => x.Guid == new Guid(guid));
            if (order == null) return;
            order.ClientID = clientId;
            DataContext.SubmitChanges();
        }

        public  int GetClientIdByUserId(string userId)
        {
            var client = DataContext.Clients.FirstOrDefault(x => x.UserID == userId);
            return client?.ClientID ?? 0;
        }
    }
}