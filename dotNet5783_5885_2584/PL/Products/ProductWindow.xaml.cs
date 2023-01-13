using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace PL;

/// <summary>
/// Interaction logic for BoProductWindow.xaml
/// </summary>
public partial class BoProductWindow : Window, INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    private ObservableCollection<object> _categories=new();
    public ObservableCollection<object> Categories
    {
        get { return _categories; }
        set
        {
            _categories = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Categories)));
        }
    }

    private BO.Product _myProduct = new();
    public BO.Product MyProduct
    {
        get { return _myProduct; }
        set
        {
            _myProduct = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyProduct)));
        }
    }

    private bool _isUpdate=false;
    public bool IsUpdate
    {
        get { return _isUpdate; }
        set
        {
            _isUpdate = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUpdate)));
        }
    }

    private Visibility _editMode = Visibility.Visible;
    public Visibility EditMode
    {
        get { return _editMode; }
        set
        {
            _editMode = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditMode)));
        }
    }

    private Visibility _displayMode = Visibility.Hidden;
    public Visibility DisplayMode
    {
        get { return _displayMode; }
        set
        {
            _displayMode = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayMode)));
        }
    }
    public BoProductWindow()
    {
        InitializeComponent();
        foreach (var cat in Enum.GetValues(typeof(BO.Category)))
        {
            Categories.Add(cat);
        }
    }
    private string _warning = "";

    public string Warning
    {
        get { return _warning; }
        set
        {
            _warning = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Warning)));
        }
    }

    public BoProductWindow(BO.ProductForList p)
    {
        (EditMode, DisplayMode) = (DisplayMode, EditMode);

        IsUpdate = true;
        try
        {
            MyProduct = bl.Product.Read(x => x?.ID == p.ID);
        }
        catch
        {
            this.Close();
        }
        Categories.Clear();
        foreach (var item in Enum.GetValues(typeof(BO.Category)))
        {
            Categories.Add(item);
        }
        InitializeComponent();
    }


    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        if (bl == null)
            throw new BO.ExceptionNullBl();
        if (MyProduct.Name == "" || MyProduct.Name == null)
            Warning = "name is not valid";
        else if(MyProduct.Price <0)
            Warning = "price must be positive number";
        else if (MyProduct.InStock < 0)
            Warning = "instock must be positive number";
        else if (MyProduct.Category == null)
            Warning = "category not selected ";
        else
        {
            try
            {
                if (IsUpdate)
                {
                    bl.Product.UpdateProduct(MyProduct);
                }
                else
                    bl.Product.AddProduct(MyProduct);
                this.Close();
            }
            catch (BO.ExceptionInvalidInput )
            {
                MessageBox.Show("Invalid input");
            }
            catch (BO.ExceptionEntityNotFound )
            {
                MessageBox.Show("product not found" );
            }
            catch (BO.ExceptionCannotCreateItem )
            {
                MessageBox.Show("can't create the product");
            }
            catch
            {
                MessageBox.Show("ERROR,try again");
            }
        }
    }
    private void Edit_Button_Click(object sender, RoutedEventArgs e)
    {
        (EditMode, DisplayMode) = (DisplayMode, EditMode);

    }
    public event PropertyChangedEventHandler? PropertyChanged;
}