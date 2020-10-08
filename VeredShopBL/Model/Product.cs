using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
    public class Product
    {
        static Product productBarcode;
        public int ProductId { get; set; }

        public string Name { get; set; }

        public long Barcode { get; set; }

        public decimal Price { get; set; }

        public int CountOnShelf { get; set; }

        public int CountInStorage { get; set; }

        public int StorekeeperId { get; set; }

        public Storekeeper Storekeeper { get; set; }

        public ICollection<Sell> Sells { get; set; }


        #region ToString
        public override string ToString()
        {
            return $"{Price,-17}{"-",-17}{"1",-17}{"-",-17}{Name}";
        }
        #endregion

        #region Return ProductId
        public override int GetHashCode()
        {
            return ProductId;
        }
        #endregion

        #region Equals Products
        public override bool Equals(object obj)
        {
            if (obj is Product product)
            {
                return ProductId.Equals(product.ProductId);
            }
            return false;
        }
        #endregion

    }
}