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

    private Visibility _isEditMode = Visibility.Visible;
    public Visibility IsEditMode
    {
        get { return _isEditMode; }
        set
        {
            _isEditMode = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEditMode)));
        }
    }

    private Visibility _isDisplayMode = Visibility.Hidden;
    public Visibility IsDisplayMode
    {
        get { return _isDisplayMode; }
        set
        {
            _isDisplayMode = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDisplayMode)));
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
        Visibility temp = IsDisplayMode;
        IsDisplayMode = IsEditMode;
        IsEditMode = temp;
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
            MessageBox.Show("Invalid input");
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




