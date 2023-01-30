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
/// Interaction logic for NewOrderWindow.xaml
/// </summary>
public partial class NewOrderWindow : Window,INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    private ObservableCollection<object> _categories = new() { };
    private BO.Cart _myCart = new();
    public BO.Cart MyCart
    {
        get { return _myCart; }
        set {
            _myCart = value;
            MyUser ??= new();
            MyUser.Cart = _myCart;
            if (MyUser != null && MyUser?.ID != 0)
                bl?.User.AddItemToCart(MyUser?.ID??0, MyUser?.Password??"", MyUser?.Cart?.Items);
        }
    }
    private BO.User _myUser = new();
    private Action<BO.User> _updateUser;
    public BO.User MyUser
    {
        get { return _myUser; }
        set { _myUser = value; 
        PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(MyUser)));
        
        }
    }
    public ObservableCollection<object> Categories
    {
        get { return _categories; }
        set
        {
            _categories = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Categories"));
        }
    }
    public object Category { get; set; }


    private ObservableCollection<BO.ProductItem> _productItemList = new() { };
    public ObservableCollection<BO.ProductItem> ProductItemList
    {
        get { return _productItemList; }
        set
        {
            _productItemList = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProductItemList"));
            }
        }
    }


    public NewOrderWindow(BO.User? user=null,Action<BO.User>? updateUser=null)
    {
        MyUser = user;
        _updateUser = updateUser;
        MyCart = user?.Cart ?? new();
        ProductItemList = new (bl.Product.ReadAllPI());
        Categories = new();
        InitializeComponent();
        foreach (var category in Enum.GetValues(typeof(BO.Category)))
        {
            Categories.Add(category);
        }
        Categories.Insert(0, "");
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (bl == null)
            throw new BO.ExceptionNullBl();
            ProductItemList = new(bl.Product.ReadAllPI((Category as BO.Category?) != null ? x => (BO.Category?)x?.Category == (BO.Category)Category : null));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void CartButton_Click(object sender, RoutedEventArgs e) 
    {
        this.Hide();
        var win = new CartWindow(MyCart, (productID, amount) =>{ MyCart = bl.Cart.UpdatePAmount(MyCart, productID, amount) ;if (MyUser.ID != 0)
            {
                
            }
        }, x => { MyCart = new(); } );
        win.ShowDialog();
        this.Show();
    }

    private void ListView_Click(object sender, RoutedEventArgs e)
    {
        if (bl == null)
            throw new BO.ExceptionNullBl();
        new ProductItemWindow((sender as ListView)!.SelectedItem as BO.ProductItem,(x,y)=>MyCart=bl.Cart.UpdatePAmount(MyCart,x,(MyCart.Items?.Where(z=>z?.ProductID==x).FirstOrDefault()??new BO.OrderItem() { Amount=0}).Amount+y)).ShowDialog();
        ProductItemList = new(bl.Product.ReadAllPI((Category as BO.Category?) != null ? x => (BO.Category?)x?.Category == (BO.Category)Category : null));
    }

    private void GroupByCategory_Click(object sender, RoutedEventArgs e)
    {
        var GropupingProducts = (from p in ProductItemList
                                group p by p.Category into catGroup
                                from pr in catGroup
                                select pr).ToList();
        ProductItemList = new(GropupingProducts);  
    }

    private void OrderByColumn_click(object sender,RoutedEventArgs e)
    {
        switch((sender as GridViewColumnHeader)?.Content)
        {
            case "Name": ProductItemList = new(ProductItemList.OrderBy(x => x.Name)); break;
            case "ID": ProductItemList = new(ProductItemList.OrderBy(x => x.ID)); break;
            case "Price": ProductItemList = new(ProductItemList.OrderBy(x => x.Price)); break;
            case "Category": ProductItemList = new(ProductItemList.OrderBy(x => x.Category)); break;
            case "In Stock": ProductItemList = new(ProductItemList.OrderBy(x => x.InStock)); break;

            default: break;
        }
    }
}
