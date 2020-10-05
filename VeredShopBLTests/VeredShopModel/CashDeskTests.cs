using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;


namespace VeredShopBL.VeredShopModel.Tests
{
    [TestClass()]
    public class CashDeskTests
    {
        

        [TestMethod()]
        public void CashDeskTest()
        {
             
            // arrange

            VeredContext dataBase = new VeredContext();

            var client1 = dataBase.Clients.Where(i => i.ClientId == 1).FirstOrDefault();

            var client2 = dataBase.Clients.Where(i => i.ClientId == 2).FirstOrDefault();

            var seller = dataBase.Sellers.Where(i => i.SellerId == 3).FirstOrDefault();

            var product1 = dataBase.Products.Where(i => i.ProductId == 4).FirstOrDefault();

            var product2 = dataBase.Products.Where(i => i.ProductId == 5).FirstOrDefault();
           
            var cart1 = new Cart(client1);
            cart1.Add(product1);
            cart1.Add(product1);
            cart1.Add(product2);

            var cart2 = new Cart(client2);
            cart2.Add(product1);
            cart2.Add(product2);
            cart2.Add(product2);

            var cashdesk1 = new CashDesk(seller, client1);
            var cashdesk2 = new CashDesk(seller, client2);


            decimal cart1ExpectedResult = 18.50M;
            decimal cart2ExpectedResult = 13.00M;


            // act

            decimal cart1ActualResult = cashdesk1.buyThroughPos(cart1);
            decimal cart2ActualResult = cashdesk2.buyThroughPos(cart2);

          //  assert

            Assert.AreEqual(cart1ExpectedResult, cart1ActualResult);
            Assert.AreEqual(cart2ExpectedResult, cart2ActualResult);
            VeredContext dataBase2 = new VeredContext();
            var product3 = dataBase2.Products.Where(i => i.ProductId == 4).FirstOrDefault();
            var product4 = dataBase2.Products.Where(i => i.ProductId == 5).FirstOrDefault();
            Assert.AreEqual(31, product3.CountOnShelf);
            Assert.AreEqual(74, product4.CountOnShelf);


        }

       
    }
}