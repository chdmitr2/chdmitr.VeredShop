using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeredShopBL.VeredShopModel;

namespace VeredShopUI.Pattern
{
    
        class ConcreteStrategyE : Strategy
        {
        private Client Client;
        private Seller Seller;
        private Storekeeper Storekeeper;

        public ConcreteStrategyE()
        {
            
        }
        public ConcreteStrategyE(Client client)
        {
            Client = client;
        }
        public ConcreteStrategyE(Seller seller)
        {
            Seller = seller;
        }
        public ConcreteStrategyE(Storekeeper storekeeper)
        {
            Storekeeper = storekeeper;
        }
        public override void AlgorithmInterface()
        {
            if (Client != null)
            {
                var Menu = new Menu(Client);
                Menu.Show();
            }
            else if (Seller != null)
            {
                var Menu = new Menu(Seller);
                Menu.Show();
            }
            else if(Storekeeper != null)
            {
                var Menu = new Menu(Storekeeper);
                Menu.Show();
            }
            else
            {
               var Menu = new Menu();
               Menu.Show();
            }
        }
    }
    
}
