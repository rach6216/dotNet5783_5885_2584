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

namespace PL
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, INotifyPropertyChanged
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        private BO.User _myUser = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public BO.User MyUser
        {
            get { return _myUser; }
            set
            {
                _myUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyUser)));
            }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }


        private Action<BO.User> _loginUser;

        private bool _isSignUp = false;
        public bool IsSignUp
        {
            get { return _isSignUp; }
            set { _isSignUp = value;
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(IsSignUp)));}
        }

        public Login(Action<BO.User> f, bool isSignUp=false)
        {
            MyUser.Cart ??= new();
           _loginUser = f;
           InitializeComponent();
           IsSignUp = isSignUp;
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyUser.Cart ??= new();
                if (IsSignUp)
                {
                    int id = bl?.User.SignUp(MyUser.UserName?? throw new NullReferenceException("invalid username"), Password, MyUser?.Cart?.CustomerName??throw new NullReferenceException("invalid name"), MyUser?.Cart?.CustomerEmail ?? throw new NullReferenceException("invalid email"), MyUser?.Cart?.CustomerAddress ?? throw new NullReferenceException("invalid address"), false)??0;
                    MyUser = bl?.User.Read(x => x?.ID == id) ?? throw new();
                }
                else
                {
                    MyUser = bl?.User.Login(MyUser.UserName ?? "", Password) ?? throw new Exception();
                }
                _loginUser(MyUser);
                this.Close();

            }
            catch (NullReferenceException exp)
            {
                MessageBox.Show(exp.Message);
            }
            catch
            {
                MessageBox.Show("wrong user name or password");
            }
        }
    }
}
