using BlApi;
using BlImplementation;
using System;
using System.Windows;

namespace PL.Products;

/// <summary>
/// Interaction logic for BoProductListWindow.xaml
/// </summary>
public partial class BoProductListWindow : Window
{
    private IBl bl = new Bl();

    public BoProductListWindow()
    {
        InitializeComponent();
        ProductsListview.ItemsSource = bl.Product.ReadAll();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
}
