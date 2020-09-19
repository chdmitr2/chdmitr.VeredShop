using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
    public class Order
    {
        public int OrderId { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int SellerId { get; set; }

        public Seller Seller { get; set; }

        public DateTime Created { get; set; }

        public ICollection<Sell> Sells { get; set; }

        #region toString
        public override string ToString()
        {
            return $"#{OrderId} to {Created.ToString("dd.MM.yy hh:mm:ss")}";
        }
        #endregion
    }
}
