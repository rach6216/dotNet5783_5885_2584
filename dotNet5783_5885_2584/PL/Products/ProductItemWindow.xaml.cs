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

    private BO.Product _myProductItem = new();
    public BO.Product MyProductItem
    {
        get { return _myProductItem; }
        set
        {
            _myProductItem = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyProductItem)));
        }
    }
    public ProductItemWindow(BO.ProductItem p)
    {
        try
        {
            MyProductItem = bl.Product.Read(x => x?.ID == p.ID);
        }
        catch
        {
            this.Close();
        }
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
