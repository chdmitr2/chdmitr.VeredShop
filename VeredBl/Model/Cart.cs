using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredBl.Model
{
    public class Cart : IEnumerable
    {
        public Client Client { get; set; }
        public Dictionary<Product, int> Products { get; set; }
        public Cart (Client client)
        {
            Client = client;
        }
        public void Add(Product product)
        {
            if(Products.TryGetValue(product,out int count) )
            {
                Products[product] = ++count;
            }
            else
            {
                Products.Add(product, 1);  
            }
           
        }

        public IEnumerator GetEnumerator()
        {
           foreach(var product in Products.Keys)
            {
                for(int i=0; i < Products[product]; i++)
                {
                    yield return product;
                }
            }
        }
    }

}
