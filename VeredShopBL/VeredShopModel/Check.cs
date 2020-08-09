using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
   public class Check
    {
        public int CheckId { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int SellerId { get; set; }
        public virtual Seller Seller { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Sell> Sells { get; set; }

        public override string ToString()
        {
            return $"#{CheckId} to {Created.ToString("dd.MM.yy hh:mm:ss")}";
        }
    }
}
