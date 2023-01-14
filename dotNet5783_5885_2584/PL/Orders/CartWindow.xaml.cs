using BO;
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
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window, INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    
    private ObservableCollection<BO.OrderItem?> _items = new ObservableCollection<BO.OrderItem?>();

    public event PropertyChangedEventHandler? PropertyChanged;
    public bool IsEmptyCart { get; private set; }=true;
    private bool _isConfirm=false;
    public bool IsConfirm { get { return _isConfirm; } set { _isConfirm = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsConfirm))); } } 
    private BO.Cart _myOrder=new ();
    public BO.Cart MyOrder
    {
        get { return _myOrder; }
        set { _myOrder = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Order)));
        }
    }
    private string _warning = "";
    private double _totalPrice ;
    public double TotalPrice { get { return _totalPrice; } set { _totalPrice = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPrice))); } }
    public string Warning
    {
        get { return _warning; }
        set
        {
            _warning = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Warning)));
        }
    }
    Action<int, int> updateAmount;
    Action<BO.Cart> cleanCart;
    public ObservableCollection<BO.OrderItem?> Items
    {
        get { return _items; }
        set
        {
            _items = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
        }
    }

    public CartWindow(BO.Cart cart,Action<int,int> upAmount,Action<BO.Cart> putCart)
    {
        cleanCart = putCart;
        MyOrder = cart;
        TotalPrice = cart.TotalPrice;
       
        updateAmount = upAmount;
        cart.Items ??= new();
        _items = new(cart.Items);
        if(Items.Count>0)
            IsEmptyCart = false;
        InitializeComponent();
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
    {
        if (Items.Count > 0)
        {
            IsConfirm = true;
            Warning = "";
        }
        else
            Warning = "No items in cart";
    }

    private void IncreaseAmount_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            updateAmount((((e.OriginalSource as Button)?.DataContext) as BO.OrderItem)?.ProductID ?? default, (((e.OriginalSource as Button)?.DataContext) as BO.OrderItem)?.Amount+1 ?? default);
            Items = new(Items);
            TotalPrice += ((((e.OriginalSource as Button)?.DataContext) as BO.OrderItem)?.Price ?? 0);
            Warning = "";
        }
        catch
        {
            Warning = "Product is out of stock";
        }
        
    }

    private void DecreaseAmount_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int amount = ((((e.OriginalSource as Button)?.DataContext) as BO.OrderItem)?.Amount ?? default) - 1;
            int productID = (((e.OriginalSource as Button)?.DataContext) as BO.OrderItem)?.ProductID ?? default;
            updateAmount(productID, amount );
            TotalPrice -= ((((e.OriginalSource as Button)?.DataContext) as BO.OrderItem)?.Price ?? 0);
            if (amount < 1)
            {
                IsEmptyCart = true;
                Items = new(Items.Where(x => x?.ProductID != productID).ToList());
            }
            else
                Items = new(Items);
            Warning = "";
        }
        catch
        {
            Warning = "Product is out of stock";
        }

    }

    private void FinishOrder_Click(object sender, RoutedEventArgs e)
    {
        MyOrder.Items = (from item in Items
                         select item).ToList();
        try
        {
            BO.Order o = bl.Cart.ConfirmOrder(MyOrder);
            new OrderWindow(o.ID).Show();

            //clean cart
            cleanCart(MyOrder);
            this.Close();
        }
        catch (BO.ExceptionInvalidInput exp)
        {
            Warning = exp.Message;
        }
    }
}
