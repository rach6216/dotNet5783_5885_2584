using BlApi;
using BlImplementation;
using System;
using System.Text.RegularExpressions;
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
            this.Close();
        }
        Category.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }


    private void Price_TextChanged(object sender, TextChangedEventArgs e)
    {
        Price.Text = Price.Text.Trim();
        //check if the input is positive double
        bool IsPDouble = Regex.IsMatch(Price.Text, @"^[0-9]+\.?[0-9]*$");
        IsPDouble = IsPDouble || Price.Text == "";
        if (!IsPDouble)
        {
            MessageBox.Show("price must be a positive number");
            Price.Clear();
        }
    }

    private void InStock_TextChanged(object sender, TextChangedEventArgs e)
    {
        InStock.Text = InStock.Text.Trim();
        //check if the input is positive int
        bool IsPInt = Regex.IsMatch(InStock.Text, "[^0-9].+");

        if (IsPInt)
        {
            MessageBox.Show("instock must be a positive integer number");
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
            _product.Name = Name.Text;
            _product.Category = (BO.Category)Category.SelectedItem;
            _product.Price = Double.Parse(Price.Text);
            _product.InStock = int.Parse(InStock.Text);
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




