#region USING
using System.Windows;
using System.Windows.Input;
#endregion

namespace VeredShopUI
{
    public partial class About : Window
    {
        #region Constructor
        public About()
        {
            InitializeComponent();
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();          
        }
        #endregion

        #region Exit from Application
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
