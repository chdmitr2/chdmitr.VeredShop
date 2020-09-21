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

        string receipt = "\t\tPurchase in Vered Shop\t\n\n" + $"{"Barcode",-30}{"Product",-20}{"Price"}\n";

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
                    receipt += $"{product.Barcode,-20}{product.Name,-20}{product.Price} \n";
                    productCount.CountInStorage--;
                    sum += product.Price;
                    
                }
            }
            receipt +=  "Total price:\t" + sum;
            dataBase.SaveChanges();

            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for the page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                graphics.DrawString(receipt, font, PdfBrushes.Black, new PointF(0, 0));

                //Save the document
                document.Save("Bill.pdf");
              
            }

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
            return sum;
        }
        #endregion
    }
}