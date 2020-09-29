using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;


namespace VeredShopBL.VeredShopModel
{
    public class CashDesk
    {
        #region Defining Objects And Variables

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

       
        string receipt = "\t\tPurchase in Vered Shop   " + DateTime.Now +  "\t\n\n" + $"{"Barcode",-30}{"Price",-25}{"Product"}\n\n";

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
                    receipt += $"{product.Barcode,-25}{product.Price,-25}{product.Name} \n";
                    productCount.CountInStorage--;
                    sum += product.Price;
                    
                }
            }
            receipt += "\nOrder Number: " + order.OrderId + " Status : Closed  Total price:\t" + sum;
            order.Amount = sum;
            dataBase.SaveChanges();

            #region Create A Bill

            using (PdfDocument document = new PdfDocument())
            {
               
                PdfPage page = document.Pages.Add();
                
                PdfGraphics graphics = page.Graphics;
              
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
              
                graphics.DrawString(receipt, font, PdfBrushes.Black, new PointF(0, 0));
              
                document.Save("Bill.pdf");
              
            }
            #endregion

            #region Send Mail With Bill To Client
            if (Client.Email != null)
            {
                try
                {
                    MailAddress from = new MailAddress("chdmitr2@gmail.com", "Vered Shop");

                    MailAddress to = new MailAddress("chdmitr2@gmail.com");

                    MailMessage mail = new MailMessage(from, to);

                    mail.Subject = "Veres Shop Purchase";

                    mail.Body = $"<h2>Hello, {Client}. You made a purchase on the date {order.Created}. The sum of purchase is {sum}. Thanks for purchase! </h2>";

                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                    smtp.Credentials = new NetworkCredential("chdmitr2@gmail.com", "algebra12");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch 
                {
                   
                }
               

            }
            #endregion

            return sum;
        }

        #endregion

        #region Buying Through POS
        public decimal buyThroughPos(Cart cart)
        {
            decimal sum = 0;

            var order = new Order()
            {
                SellerId = Seller.SellerId,

                ClientId = 5,

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
                    receipt += $"{product.Barcode,-20}{product.Price,-20}{product.Name} \n";
                    productCount.CountInStorage--;
                    sum += product.Price;

                }
            }
            receipt += "Order Number: " + order.OrderId + "Status : Closed   Total price:\t" + sum;
            order.Amount = sum;
            dataBase.SaveChanges();

            #region Create A Bill

            using (PdfDocument document = new PdfDocument())
            {

                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                graphics.DrawString(receipt, font, PdfBrushes.Black, new PointF(0, 0));

                document.Save("Bill.pdf");

            }
            #endregion

            return sum;
        }
     #endregion


    }
}