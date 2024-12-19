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
        public Frame MainFrame { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            mainFrame = new Frame();
            Content = mainFrame;
            mainFrame.Navigate(new ShopPage());
        }
    }
}