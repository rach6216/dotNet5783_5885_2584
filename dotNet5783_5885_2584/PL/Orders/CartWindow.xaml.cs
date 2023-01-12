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
    public bool IsFullCart { get; private set; }=true;
    public ObservableCollection<BO.OrderItem?> Items
    {
        get { return _items; }
        set
        {
            _items = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }
    }

    public CartWindow(BO.Cart cart)
    {
        cart.Items ??= new();
        _items = new(cart.Items);
        if(Items.Count>0)
            IsFullCart = false;
        InitializeComponent();
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
    {

    }
}
