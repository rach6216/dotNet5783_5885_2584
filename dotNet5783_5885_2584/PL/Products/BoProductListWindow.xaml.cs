using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
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
        List<BO.Category?> l = new List<BO.Category?>() { };
    
        //foreach (var category in Enum.GetValues(typeof(BO.Category)))
        //{
        //    l.Add((BO.Category?)category);
        //}
        //l.Add(null);
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    
        ProductsListview.ItemsSource = bl.Product.ReadAll();
    }

    private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        BO.Category category = (BO.Category)CategorySelector.SelectedItem;
        ProductsListview.ItemsSource = bl.Product.ReadAll(category!=null?x=>(BO.Category)x.Value.Category==category:null);
    }

    private void Button_Click(object sender, RoutedEventArgs e)=> new BoProductWindow(bl).Show();

}
