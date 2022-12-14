using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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
        List<object> l = new() { };
        l.Add("");
        foreach (var category in Enum.GetValues(typeof(BO.Category)))
        {
            l.Add(category);
        }
        CategorySelector.ItemsSource = l;
        ProductsListview.ItemsSource = bl.Product.ReadAll();

    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        var category = CategorySelector.SelectedItem;
        
            ProductsListview.ItemsSource = bl.Product.ReadAll(category !=(object)"" ? x => (BO.Category?)x!.Value.Category == (BO.Category)category : null);

    }

    private void AddProductButton_click(object sender, RoutedEventArgs e)
    {
        new BoProductWindow().ShowDialog();
        var category = CategorySelector.SelectedItem;
        ProductsListview.ItemsSource = bl.Product.ReadAll(category != null ? x => (BO.Category?)x!.Value.Category == (BO.Category)category : null);
    }

    private void ProductsListview_doubleClicked(object sender, MouseButtonEventArgs e)
    {
        new BoProductWindow((sender as ListView)!.SelectedItem as BO.ProductForList).ShowDialog();
        object cat = CategorySelector.SelectedItem;
        ProductsListview.ItemsSource = bl.Product.ReadAll(cat != null ? x => (BO.Category?)x!.Value.Category == (BO.Category)cat : null);

    }

}
