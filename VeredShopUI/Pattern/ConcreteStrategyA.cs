using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeredShopBL.VeredShopModel;

namespace VeredShopUI.Pattern
{
    class ConcreteStrategyA : Strategy
    {
        private Client Client;
        public ConcreteStrategyA(Client client)
        {
            Client = client;
        }        
        public override void AlgorithmInterface()
        {
            if (Client != null)
            {
                var selfPurchase = new SelfPurchase(Client);
                selfPurchase.Show();
            }
            else
            {
                var selfPurchase = new SelfPurchase();
                selfPurchase.Show();
            }
        }
    }
}
 