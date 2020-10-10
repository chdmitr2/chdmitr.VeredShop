using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeredShopBL.VeredShopModel;

namespace VeredShopUI.Pattern
{
    class ConcreteStrategyB : Strategy
    {
        private Seller Seller;
        public ConcreteStrategyB(Seller seller)
        {
            Seller = seller;
        }
        public override void AlgorithmInterface()
        {
            if (Seller != null)
            {
                var buyThroughPos = new buyThroughPos(Seller);
                buyThroughPos.Show();
            }
            else
            {
                var buyThroughPos = new buyThroughPos();
                buyThroughPos.Show();
            }
        }
    }
}
