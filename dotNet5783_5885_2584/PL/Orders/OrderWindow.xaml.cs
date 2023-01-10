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
public partial class OrderWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    private BO.Order _myOrder = new();
    public BO.Order MyOrder
    {
        get { return _myOrder; }
        set {_myOrder = value;}
    }
    public OrderWindow()
    {
        InitializeComponent();
    }
    public OrderWindow(BO.OrderForList? o)
    {
        try
        {
            MyOrder = bl.Order.Read(x => x?.ID == o.ID);
        }
        catch
        {
            MessageBox.Show("hhh");
            //this.Close();
        }
        InitializeComponent();
    }
}
