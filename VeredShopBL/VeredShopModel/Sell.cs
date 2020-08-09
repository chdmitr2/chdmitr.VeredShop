using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
    public class Sell
    {
        public int SellId { get; set; }
        public int CheckId { get; set; }
        public int ProductId { get; set; }
        public virtual Check Check { get; set; }      
        public virtual Product Product { get; set; }
       
    }
}
