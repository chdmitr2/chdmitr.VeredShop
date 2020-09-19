using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
    public class CashDesk
    {
        VeredContext dataBase = new VeredContext();

        Seller Seller;

        Client Client;

        public CashDesk(Seller seller)
        {
            Seller = seller;
        }

        public CashDesk(Client client)
        {
            Client = client;
        }

        public decimal Dequeue(Cart cart)
        {
            decimal sum = 0;

            var order = new Order()
            {
                SellerId = 1,

                ClientId = Client.ClientId,

                Created = DateTime.Now
            };

            dataBase.Orders.Add(order);
            dataBase.SaveChanges();


            var sells = new List<Sell>();

            foreach (Product product in cart)
            {
                if (product.CountInStorage > 0)
                {
                    var sell = new Sell()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.ProductId,
                    };
                    dataBase.Sells.Add(sell);
                    product.CountInStorage--;
                    sum += product.Price;
                    
                }
            }
            dataBase.SaveChanges();
            return sum;
        }
    }
}