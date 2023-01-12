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
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window, INotifyPropertyChanged   
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    private BO.Order _myOrder = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    public BO.Order MyOrder
    {
        get { return _myOrder; }
        set {_myOrder = value;
        if(PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(nameof(MyOrder)));
            }
        }
    }

    private Visibility _adminDisplay = Visibility.Hidden;
    public  Visibility AdminDisplay
    {
        get { return _adminDisplay; }
        set { _adminDisplay = value;
            if (PropertyChanged!=null)
            {
                PropertyChanged (this,new PropertyChangedEventArgs(nameof(AdminDisplay)));
            }
        }
    }

    private bool _canShip;
    public bool CanShip
    {
        get { return _canShip; }
        set
        {
            _canShip = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CanShip)));
            }
        }
    }
    private bool _canDelivery;
    public bool CanDelivery
    {
        get { return _canDelivery; }
        set
        {
            _canDelivery = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CanDelivery)));
            }
        }
    }

    void CheckStatus()
    {
        AdminDisplay=Visibility.Visible;

        if (MyOrder.Status == BO.OrderStatus.OrderIsConfirmed)
        {
            CanShip = true;
            CanDelivery = false;
        }
        else if (MyOrder.Status == BO.OrderStatus.OrderIsShiped)
        {
            CanShip = false;
            CanDelivery = true;
        }
        else
        {
            CanShip = false;
            CanDelivery = false;
        }
    }
    public OrderWindow()
    {
        InitializeComponent();
    }
    public OrderWindow(int id)
    {
        try
        {
            MyOrder = bl.Order.Read(x => x?.ID == id);
            InitializeComponent();
        }
        catch
        {
            MessageBox.Show("hhh");
            //this.Close();
        }
    }
    public OrderWindow(BO.OrderForList? o)
    {
        try
        {
            MyOrder = bl.Order.Read(x => x?.ID == o?.ID);
        }
        catch
        {
            MessageBox.Show("hhh");
            //this.Close();
        }
        CheckStatus();
        InitializeComponent();
    }

    private void Ship_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Order.ShipOrder(MyOrder.ID);
            CanShip = false;
            CanDelivery = true;
            MyOrder = bl.Order.Read(x => x?.ID == MyOrder?.ID);
        }
        catch
        {
            MessageBox.Show("error");
        }
    }

    private void Delivery_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Order.DeliveryOrder(MyOrder.ID);
            CanDelivery = false;
            MyOrder = bl.Order.Read(x => x?.ID == MyOrder?.ID);
        }
        catch
        {
            MessageBox.Show("error");
        }
    }
}
