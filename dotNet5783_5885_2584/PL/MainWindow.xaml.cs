using BlApi;
using PL.Products;
using System.Windows;
using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void productListButton_Click(object sender, RoutedEventArgs e) => new BoProductListWindow().Show();

        private IBl bl = new Bl();
    }
}
