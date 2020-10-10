using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopUI.Pattern
{
    class ConcreteStrategyF : Strategy
    {
        public override void AlgorithmInterface()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
