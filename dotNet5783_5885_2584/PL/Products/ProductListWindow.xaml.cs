using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;
using System.Xml.Linq;

namespace PL.Products;

/// <summary>
/// Interaction logic for BoProductListWindow.xaml
/// </summary>
public partial class BoProductListWindow : Window, INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    private ObservableCollection<object> _categories;
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
   
    private ObservableCollection<BO.ProductForList> _productList;
    public ObservableCollection<BO.ProductForList> ProductList
    {
        get { return _productList; }
        set
        {
            _productList = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProductList"));
            }
        }
    }


    public BoProductListWindow()
    {
        ProductList = new(bl.Product.ReadAll());
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
        ProductList = new( bl.Product.ReadAll((Category as BO.Category?) != null ? x => (BO.Category?)x?.Category == (BO.Category)Category : null));
    }

    private void AddProductButton_click(object sender, RoutedEventArgs e)
    {
        if (bl == null)
            throw new BO.ExceptionNullBl();
        new BoProductWindow().ShowDialog();
        ProductList = new(bl.Product.ReadAll((Category as BO.Category?) != null ? x => (BO.Category?)x?.Category == (BO.Category)Category : null));
    }

    private void ProductsListview_doubleClicked(object sender, MouseButtonEventArgs e)
    {
        if (bl == null)
            throw new BO.ExceptionNullBl();
        new BoProductWindow((sender as ListView)!.SelectedItem as BO.ProductForList).ShowDialog();
        ProductList = new(bl.Product.ReadAll((Category as BO.Category?) != null ? x => (BO.Category?)x?.Category == (BO.Category)Category : null));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}