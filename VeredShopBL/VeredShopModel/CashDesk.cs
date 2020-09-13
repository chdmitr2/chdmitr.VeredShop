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
        public int Number { get; set; }
        public Seller Seller { get; set; }
        public Queue<Cart> Queue { get; set; }

        public int MaxQueueLength { get; set; }
        public int ExitClient { get; set; }
       

        public CashDesk(int number,Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            
        }

        public void Enqueue(Cart cart)
        {
            if(Queue.Count <= MaxQueueLength )
            {
                Queue.Enqueue(cart);

            }
            else
            {
                ExitClient++;
            }
        }
        public decimal Dequeue()
        {
            decimal sum = 0;
            var card = Queue.Dequeue();
            if(card != null)
            {
                var check = new Check()
                {
                    SellerId = Seller.SellerId,
                    Seller = Seller,
                    ClientId = card.Client.ClientId,
                    Client = card.Client,
                    Created = DateTime.Now
                };
               
                    dataBase.Checks.Add(check);
                    dataBase.SaveChanges();
                    check.CheckId = 0;
                
                var sells = new List<Sell>();

                foreach(Product product in card)
                {
                    if (product.CountInStorage > 0)
                    {


                        var sell = new Sell()
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            Barcode = product.Barcode,
                            Product = product
                        };

                            dataBase.Sells.Add(sell);
                     

                        product.CountInStorage--;
                        sum += product.Price;
                    }
                }

                    dataBase.SaveChanges();
               
            }
            return sum;
        }
    }
}
