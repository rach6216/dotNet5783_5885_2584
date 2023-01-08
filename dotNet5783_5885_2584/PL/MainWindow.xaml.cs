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
    private void productListButton_Click(object sender, RoutedEventArgs e) => new adminWindow().Show();

    private BlApi.IBl? bl = BlApi.Factory.Get();
}
