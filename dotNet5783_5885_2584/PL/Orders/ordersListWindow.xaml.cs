using BO;
using PL;
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
/// Interaction logic for ordersListWindow.xaml
/// </summary>
public partial class ordersListWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    private ObservableCollection<BO.OrderForList> _orderList = new() { };
    public ObservableCollection<BO.OrderForList> OrderList
    {
        get { return _orderList; }
        set { _orderList = value; }
    }
    public ordersListWindow()
    {
        //convert???????????
        //OrderList = (ObservableCollection<BO.OrderForList>) bl.Order.ReadAll();
        InitializeComponent();
    }

    private void orderListView_doubleClicked(object sender, MouseButtonEventArgs e)
    {
        if (bl == null)
            throw new BO.ExceptionNullBl();
        new OrderWindow((sender as ListView)!.SelectedItem as BO.OrderForList).ShowDialog();
        OrderList = new(bl.Order.ReadAll() as ObservableCollection<BO.OrderForList>);
    }

}
