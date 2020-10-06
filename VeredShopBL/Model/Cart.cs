using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
    public class Cart : IEnumerable
    {
        public Client Client { get; set; }

        public Seller Seller { get; set; }

        public Dictionary<Product, int> Products { get; set; }

        public decimal Price => GetAll().Sum(p => p.Price);

        public Cart()
        {           
            Products = new Dictionary<Product, int>();
        }
        public Cart(Client client)
        {
            Client = client;
            Products = new Dictionary<Product, int>();
        }
        public Cart(Seller seller)
        {
            Seller = seller;
            Products = new Dictionary<Product, int>();
        }


        public void Add(Product product)
        {
            if (Products.TryGetValue(product, out int count))
            {
                Products[product] = ++count;
            }
            else
            {
                Products.Add(product, 1);
            }
        }

        public void Remove(Product product)
        {
            if (Products.TryGetValue(product, out int count))
            {
                Products[product] = --count;
            }
            else
            {
                Products.Remove(product);               
            }
        }


        public IEnumerator GetEnumerator()
        {
            foreach (var product in Products.Keys)
            {
                for (int i = 0; i < Products[product]; i++)
                {
                    yield return product;
                }
            }
        }

        public List<Product> GetAll()
        {
            var result = new List<Product>();

            foreach (Product pr in this)
            {
                result.Add(pr);
            }
            return result;
        }

    }
}