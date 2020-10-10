#region USING
using System.Windows;
using System.Windows.Input;
using VeredShopBL.VeredShopModel;
using VeredShopUI.Pattern;
#endregion

namespace VeredShopUI
{
    public partial class Menu : Window
    {
        #region Defining Objects
        VeredContext dataBase;
        Context context;
        Client client1;
        Seller seller1;
        Storekeeper storekeeper1;
        #endregion

        #region Constructors
        public Menu()
        {
            InitializeComponent();
            dataBase = new VeredContext();
        }
        public Menu (Client client)
        {
            InitializeComponent();
            dataBase = new VeredContext();
            client1 = client;
            btnBuyThrroughPos.Visibility = Visibility.Hidden;
            btnStorage.Visibility = Visibility.Hidden;
            btnAllDataShop.Visibility = Visibility.Hidden;
            imStorage.Visibility = Visibility.Hidden;
        }

        public Menu(Seller seller)
        {
            InitializeComponent();
            dataBase = new VeredContext();
            seller1 = seller;
            btnStorage.Visibility = Visibility.Hidden;
            btnAllDataShop.Visibility = Visibility.Hidden;
            imStorage.Visibility = Visibility.Hidden;

        }
        public Menu(Storekeeper storekeeper)
        {
            InitializeComponent();
            dataBase = new VeredContext();
            storekeeper1 = storekeeper;
            btnBuyThrroughPos.Visibility = Visibility.Hidden;
            btnAllDataShop.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Allows A Window To Be Dragged By  A Mouse
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region Back To Main Window
        private void OnMain_Click(object sender, RoutedEventArgs e)
        {
            context = new Context(new ConcreteStrategyF());
            context.ContextInterface();
            this.Close();
        }
        #endregion

        #region Go To Buy Through POS Screen
        private void buyThroughPos_Click(object sender, RoutedEventArgs e)
        {
            context = new Context(new ConcreteStrategyB(seller1));
            context.ContextInterface();
            this.Close();
        }
        #endregion

        #region Exit From Aplication
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Go To Storage Screen
        private void Storage_Click(object sender, RoutedEventArgs e)
        {
            context = new Context(new ConcreteStrategyC(storekeeper1));
            context.ContextInterface();
            this.Close();
        }
        #endregion

        #region Go To Users Screen
        private void Users_Click(object sender, RoutedEventArgs e)
        {
            context = new Context(new ConcreteStrategyD());
            context.ContextInterface();
            this.Close();
        }
        #endregion

        #region Go To Self Purchase Screen
        private void Self_Purchase_Click(object sender, RoutedEventArgs e)
        {           
                context = new Context(new ConcreteStrategyA(client1));
                context.ContextInterface();
                this.Close();          
        }
        #endregion
    }
}
