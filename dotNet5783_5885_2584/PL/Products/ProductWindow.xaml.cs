using BlApi;
using BlImplementation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

/// <summary>
/// Interaction logic for BoProductWindow.xaml
/// </summary>
public partial class BoProductWindow : Window
{
    private IBl bl = new Bl();
    private BO.Product _product = new();
    private bool isUpdate;
    public BoProductWindow()
    {
        InitializeComponent();
        Category.ItemsSource = Enum.GetValues(typeof(BO.Category));
        ID.Visibility = Visibility.Collapsed;
    }

    public BoProductWindow(BO.ProductForList p)
    {
        InitializeComponent();
        isUpdate = true;
        AddProductButton.Content = "UPDATE";
        try
        {
            _product = bl.Product.Read(x => x?.ID == p.ID);
            Name.Text = _product.Name;
            Price.Text = _product.Price.ToString();
            Category.SelectedItem = _product.Category;
            InStock.Text = _product.InStock.ToString();
            Id.Content = _product.ID.ToString();

        }
        catch
        {

        }
        Category.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void Name_TextChanged(object sender, TextChangedEventArgs e)
    {
        Name.Text = Name.Text.Trim();
        _product.Name = Name.Text;
    }

    private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _product.Category = (BO.Category)Category.SelectedItem;
    }

    private void Price_TextChanged(object sender, TextChangedEventArgs e)
    {
        Price.Text = Price.Text.Trim();
        try
        {
            if (Price.Text.Length > 0)
                _product.Price = double.Parse(Price.Text);
        }
        catch
        {
            MessageBox.Show("price must be a number");
            Price.Clear();
        }
    }

    private void InStock_TextChanged(object sender, TextChangedEventArgs e)
    {
        InStock.Text = InStock.Text.Trim();
        try
        {
            if (InStock.Text.Length > 0)
                _product.InStock = int.Parse(InStock.Text);
        }
        catch
        {
            MessageBox.Show("instock must be a number");
            InStock.Clear();
        }
    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        if (Name.Text == "" || Price.Text == "" || InStock.Text == null || Category.SelectedItem == null)
        {
            MessageBox.Show("Invalid input");
        }
        else
        {
            try
            {
                if (isUpdate)
                {
                    bl.Product.UpdateProduct(_product);
                    
                }
                else
                    bl.Product.AddProduct(_product);
                this.Close();
            }
            catch (BO.ExceptionInvalidInput exp)
            {
                MessageBox.Show("Invalid input" + exp.Message);
            }
            catch (BO.ExceptionEntityNotFound exp)
            {
                MessageBox.Show("can't find the product" + exp.Message);
            }
            catch (BO.ExceptionCannotCreateItem exp)
            {
                MessageBox.Show("can't create the product" + exp.Message);
            }
            catch
            {
                MessageBox.Show("ERROR,try again");
            }
        }
    }


}




