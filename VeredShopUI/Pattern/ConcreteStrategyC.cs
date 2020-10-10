using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeredShopBL.VeredShopModel;

namespace VeredShopUI.Pattern
{
    class ConcreteStrategyC : Strategy
    {
        private Storekeeper  Storekeeper;
        public ConcreteStrategyC(Storekeeper storekeeper)
        {
            Storekeeper = storekeeper;
        }
        public override void AlgorithmInterface()
        {
            if (Storekeeper != null)
            {
                var Storage = new Storage(Storekeeper);
                Storage.Show();
            }
            else
            {
                var Storage = new Storage();
                Storage.Show();
            }
        }
    }
}
