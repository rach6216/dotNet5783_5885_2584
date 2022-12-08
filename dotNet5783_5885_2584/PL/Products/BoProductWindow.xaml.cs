using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for BoProductWindow.xaml
    /// </summary>
    public partial class BoProductWindow : Window
    {
        private IBl bl = new Bl();
        private BO.Product _product = new();

        public BoProductWindow(BO.ProductForList? p=null)
        {
            InitializeComponent();
            if (p != null)
            {
                AddProductButton.Content = "UPDATE";
                try
                {
                    _product = bl.Product.Read(x => x!.Value.ID == p.ID);
                    Name.Text = _product.Name;
                    Price.Text = _product.Price.ToString();
                    Category.SelectedItem = _product.Category;
                    InStock.Text = _product.InStock.ToString();
                }
                catch
                {

                }
                                
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
            bl.Product.AddProduct(_product);
            this.Close();
        }

       
    }
}
