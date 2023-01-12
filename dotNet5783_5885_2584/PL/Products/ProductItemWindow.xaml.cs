using BO;
using System;
using System.Collections.Generic;
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
/// Interaction logic for ProductItemWindow.xaml
/// </summary>
public partial class ProductItemWindow : Window, INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    private BO.ProductItem _myProductItem = new();
    private Action<int,int> _addProduct;
    public BO.ProductItem MyProductItem
    {
        get { return _myProductItem; }
        set
        {
            _myProductItem = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyProductItem)));
        }
    }
    private int _numValue = 0;

    public int NumValue
    {
        get { return _numValue; }
        set
        {
            if (value >= 0)
            {
                _numValue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(NumValue)));
                }
            }
            
        }
    }
    private void cmdUp_Click(object sender, RoutedEventArgs e)
    {
        NumValue=NumValue+1;
    }

    private void cmdDown_Click(object sender, RoutedEventArgs e)
    {
        NumValue=NumValue-1;
    }
    public ProductItemWindow(BO.ProductItem p,Action<int,int>? f)
    {
        try
        {
            MyProductItem = p;
            _addProduct = f;
        }
        catch
        {
            this.Close();
        }
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void AddToCart_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_addProduct != null)
            {
                _addProduct(MyProductItem.ID, NumValue);
                this.Close();
            }
        }
        catch(ExceptionProductOutOfStock )
        {
            MessageBox.Show("product is out of stock");
        }
    }
}
