using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


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
        List<object> l = new () { };

        foreach (var category in Enum.GetValues(typeof(BO.Category)))
        {
            l.Add((object)category);
        }
        l.Insert(0, "");
        CategorySelector.ItemsSource = l;
        ProductsListview.ItemsSource = bl.Product.ReadAll();
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       
        var category = CategorySelector.SelectedItem;
        ProductsListview.ItemsSource = bl.Product.ReadAll(category!=(object)""?x=>(BO.Category?)x!.Value.Category==(BO.Category)category:null);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    { 
        new BoProductWindow().ShowDialog();
        var category =(object) CategorySelector.Text;
        ProductsListview.ItemsSource = bl.Product.ReadAll(category != (object)"" ? x => (BO.Category?)x!.Value.Category == (BO.Category)category : null);
    }

    private void ProductsListview_doubleClicked(object sender, MouseButtonEventArgs e)
    {
        new BoProductWindow((sender as ListView)!.SelectedItem as BO.ProductForList).ShowDialog();
        ProductsListview.ItemsSource = bl.Product.ReadAll();

    }

}
