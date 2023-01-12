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

    void ChangeDisplayMode()
    {
        Visibility temp = DisplayMode;
        DisplayMode = EditMode;
        EditMode = temp;
    }

    private String _warning = "OK";

    public String Warning
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
        ChangeDisplayMode();
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
        if (MyProduct.Name == "" || MyProduct.Price <0 || MyProduct.InStock < 0 || MyProduct.Category == null)
        {
            Warning = "invalid input";
        }
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
            catch (BO.ExceptionInvalidInput exp)
            {
                MessageBox.Show("Invalid input" + exp.Message);
            }
            catch (BO.ExceptionEntityNotFound exp)
            {
                MessageBox.Show("can't find the product" + exp.Message);
            }
            catch (BO.ExceptionCannotCreateItem exp)
            {
                MessageBox.Show("can't create the product" + exp.Message);
            }
            catch
            {
                MessageBox.Show("ERROR,try again");
            }
        }
    }
    private void Edit_Button_Click(object sender, RoutedEventArgs e)
    {
        ChangeDisplayMode();
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}