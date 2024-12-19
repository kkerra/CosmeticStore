using CosmeticStore.Pages;
using System.Windows;
using System.Windows.Controls;

namespace CosmeticStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ShopPage());
        }
    }
}