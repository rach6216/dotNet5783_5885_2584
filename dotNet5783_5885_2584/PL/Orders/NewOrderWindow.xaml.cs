using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for NewOrderWindow.xaml
/// </summary>
public partial class NewOrderWindow : Window,INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    private ObservableCollection<object> _categories = new() { };
    internal BO.Cart MyCart=new();
    public ObservableCollection<object> Categories
    {
        get { return _categories; }
        set
        {
            _categories = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Categories"));
            }
        }
    }
    public object Category { get; set; }


    private ObservableCollection<BO.ProductItem> _productItemList = new() { };
    public ObservableCollection<BO.ProductItem> ProductItemList
    {
        get { return _productItemList; }
        set
        {
            _productItemList = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProductItemList"));
            }
        }
    }


    public NewOrderWindow()
    {
        ProductItemList = new (bl.Product.ReadAllPI());
        Categories = new();
        InitializeComponent();
        foreach (var category in Enum.GetValues(typeof(BO.Category)))
        {
            Categories.Add(category);
        }
        Categories.Insert(0, "");
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (bl == null)
            throw new BO.ExceptionNullBl();
            ProductItemList = new(bl.Product.ReadAllPI((Category as BO.Category?) != null ? x => (BO.Category?)x?.Category == (BO.Category)Category : null));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void cartButton_Click(object sender, RoutedEventArgs e) => new CartWindow(MyCart).Show();

    private void ListView_Click(object sender, RoutedEventArgs e)
    {
        if (bl == null)
            throw new BO.ExceptionNullBl();
        new ProductItemWindow((sender as ListView)!.SelectedItem as BO.ProductItem,x=>bl.Cart.AddProduct(MyCart,x)).ShowDialog();
        ProductItemList = new(bl.Product.ReadAllPI((Category as BO.Category?) != null ? x => (BO.Category?)x?.Category == (BO.Category)Category : null));
    }

    private void GroupByCategory_Click(object sender, RoutedEventArgs e)
    {
        var GropupingProducts = (from p in ProductItemList
                                group p by p.Category into catGroup
                                from pr in catGroup
                                select pr).ToList();
        ProductItemList = new(GropupingProducts);  
    }
}
