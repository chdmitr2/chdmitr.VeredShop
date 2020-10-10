using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopUI.Pattern
{
    class ConcreteStrategyD: Strategy
    {
        public override void AlgorithmInterface()
        {
            var shopUsers = new User();
            shopUsers.Show();
        }
    }
}
