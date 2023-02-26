using System.ComponentModel;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window,INotifyPropertyChanged
{
    
    public MainWindow()
    {
        _isAdmin = false;
        InitializeComponent();
    }
    private void productListButton_Click(object sender, RoutedEventArgs e) => new AdminWindow().Show();

    private BlApi.IBl? bl = BlApi.Factory.Get();
    private BO.User _myUser;
    private bool _isAdmin=false;
    public bool IsAdmin
    {
        get { return _isAdmin; }
        set { _isAdmin = value; 
        PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(IsAdmin)));}
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public BO.User MyUser
    {
        get { return _myUser; }
        set {
            if (_myUser!=null&&_myUser.IsAdmin != IsAdmin)
                IsAdmin = !IsAdmin;
            _myUser = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyUser)));
        }
    }
    private void NewOrderButton_Click(object sender, RoutedEventArgs e) => new NewOrderWindow(MyUser,user=>MyUser=user).ShowDialog();

    private void OrderTrackingButton_Click(object sender, RoutedEventArgs e) => new OrderTrackingWindow().ShowDialog();

    private void Login_click(object sender, RoutedEventArgs e)
    {
        new Login(x=>MyUser=x).ShowDialog();
    }

    private void SignUp_click(object sender, RoutedEventArgs e)
    {
        new Login(x => MyUser = x,true).ShowDialog();

    }

    private void simulatorButton_Click(object sender, RoutedEventArgs e)
    {
        new SimulatorWindow().ShowDialog();
    }
}
