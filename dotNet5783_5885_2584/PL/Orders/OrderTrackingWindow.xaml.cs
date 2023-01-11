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
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window,INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    private BO.OrderTracking _orderTracking=new();
    public BO.OrderTracking OrderTracking
    {
        get { return _orderTracking; }
        set { _orderTracking = value;
        if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(OrderTracking)));
            }
        }
    }
    private ObservableCollection<Tuple<DateTime?, string>> _tracking = new();
    public ObservableCollection<Tuple<DateTime?, string>> Tracking { 

        get { return _tracking; }
        set { _tracking = value; 
        if(PropertyChanged!=null)
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Tracking)));
        }
    }
    
    private int? _orderID;
    public int?
        OrderID
    {
        get { return _orderID; }
        set
        {
            _orderID = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(OrderID)));
            }
        }
    }

    public OrderTrackingWindow()
    {
        
        InitializeComponent();
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void TrackOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {if (OrderID != null)
            {
               // bl.Order.ShipOrder((int)OrderID);
                OrderTracking = bl.Order.OrderTracking((int)OrderID);
                Tracking = new(OrderTracking.Tracking);

            }
            else
            {
                MessageBox.Show("Order not exist");
            }
        }
        catch
        {
            OrderTracking=new();
            Tracking = new();
        }
    }

    private void OrderDetails_Click(object sender, RoutedEventArgs e)
    {
        if (OrderTracking != null)
            new OrderWindow((int)OrderID).ShowDialog();
        else
            MessageBox.Show("order not exist");
    }
}
