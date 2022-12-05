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
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        ProductsListview.ItemsSource = bl.Product.ReadAll();
    }

    private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        BO.Category category = (BO.Category)CategorySelector.SelectedItem;
        ProductsListview.ItemsSource = bl.Product.ReadAll(x=>(BO.Category)x.Value.Category==category);

    }
}
