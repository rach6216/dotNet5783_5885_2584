using PL;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void productListButton_Click(object sender, RoutedEventArgs e) => new AdminWindow().Show();

    private BlApi.IBl? bl = BlApi.Factory.Get();

    private void newOrderButton_Click(object sender, RoutedEventArgs e) => new NewOrderWindow().Show();

    private void orderTrackingButton_Click(object sender, RoutedEventArgs e) => new OrderTrackingWindow().Show();
}
