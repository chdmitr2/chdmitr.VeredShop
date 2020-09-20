using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace VeredShopBL.VeredShopModel
{
    public class CashDesk
    {
        #region Defining Objects
        VeredContext dataBase = new VeredContext();

        Seller Seller;

        Client Client;
        #endregion

        #region Constructors
        public CashDesk(Seller seller)
        {
            Seller = seller;
        }

        public CashDesk(Client client)
        {
            Client = client;
        }
        #endregion

        #region Self Purchase
        public decimal SelfPurchase(Cart cart)
        {
            decimal sum = 0;

            var order = new Order()
            {
                SellerId = 4,

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
                    long barcode = product.Barcode;
                    var productCount = dataBase.Products.Where(i => i.Barcode == barcode).FirstOrDefault();
                    productCount.CountInStorage--;
                    sum += product.Price;
                    
                }
            }
            dataBase.SaveChanges();

            if (Client.Email != null)
            {
                try
                {
                    MailAddress from = new MailAddress("dimachudnovsky@walla.com", "Vered Shop");

                    MailAddress to = new MailAddress("chdmitr2@gmail.com");

                    MailMessage mail = new MailMessage(from, to);

                    mail.Subject = "Veres Shop Purchase";

                    mail.Body = $"<h2>Hello, {Client}. You made a purchase on the date {order.Created}. The sum of purchase is {sum}. Thanks for purchase! </h2>";

                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                    smtp.Credentials = new NetworkCredential("dimachudnovsky@walla.com", "algebra12");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch 
                {
                   
                }
            }
            return sum;
        }
        #endregion
    }
}