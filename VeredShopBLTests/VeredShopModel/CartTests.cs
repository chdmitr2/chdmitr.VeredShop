using Microsoft.VisualStudio.TestTools.UnitTesting;
using VeredShopBL.VeredShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel.Tests
{
    [TestClass()]
    public class CartTests
    {
        [TestMethod()]
        public void CartTest()
        {
            // arrange
            var client = new Client()
            {
                ClientId = 1,
                FirstName = "testUser"
            };
            var product1 = new Product()
            {
                ProductId = 1,
                Name = "pr1",
                Price = 100,
                CountOnShelf = 10,
                CountInStorage = 100
            }; 
            var product2 = new Product()
            {
                ProductId = 2,
                Name = "pr2",
                Price = 200,
                CountOnShelf = 20,
                CountInStorage = 100
            };
            var cart = new Cart(client);
           
            var expectedResult = new List<Product>()
            {
                product1,product1,product2
            };

            // act
            cart.Add(product1);
            cart.Add(product1);
            cart.Add(product2);

            var cartResult = cart.GetAll();

            // assert
           
            Assert.AreEqual(expectedResult.Count, cartResult.Count);
            for(int i = 0;i < expectedResult.Count;i++)
            {
                Assert.AreEqual(expectedResult[i], cartResult[i]);
            }

        }

       
    }
}