using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyNotes.Models;
using MyNotes.Services.IServices;

namespace MyNotes.UI;

public partial class SignUpPage : UserControl
{
    private MainWindow _mainWindow;
    private IUserService _userService;
    public SignUpPage()
    {
        InitializeComponent();
    }

    public void SetMainWindow(MainWindow mainWindow,IUserService userService)
    {
        _mainWindow = mainWindow;
        _userService = userService;
    }

    private async void SignUp_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var user = new User()
        {
            FirstName = firstname_txt.Text,
            LastName = lastname_txt.Text,
            PhoneNumber = phoneNumber_txt.Text,
            Password = Password_box.Password
        };
        if (email_txt.Text.Contains("@gmail.com"))
        {
            user.Email = email_txt.Text;
        }
        else
        {
            MessageBox.Show("Email was entered incorrectly!");
        }

        if (firstname_txt.Text == string.Empty || lastname_txt.Text == string.Empty ||
            phoneNumber_txt.Text == string.Empty || Password_box.Password == string.Empty)
        {
            MessageBox.Show("Please fill in the fields!");
        }
        
        await _userService.AddUser(user);
        firstname_txt.Text = lastname_txt.Text =
            email_txt.Text = phoneNumber_txt.Text = Password_box.Password = string.Empty;
        _mainWindow.SignUp_doc.Visibility = Visibility.Collapsed;
        _mainWindow.Pin_doc.Visibility = Visibility.Visible;
    }

    private void Hidpass_btn_OnClick(object sender, RoutedEventArgs e)
    {
        string pin = Password_box.Password;
        Text_box.Text=pin;
        Text_box.Visibility = Visibility.Visible;;
        Password_box.Visibility = Visibility.Collapsed;
        
        hidpass_btn.Visibility = Visibility.Collapsed;
        showpass_btn.Visibility = Visibility.Visible;
    }

    private void Showpass_btn_OnClick(object sender, RoutedEventArgs e)
    {
        Text_box.Text= Password_box.Password;
        Password_box.Visibility = Visibility.Visible;
        Text_box.Visibility = Visibility.Collapsed;
        
        hidpass_btn.Visibility = Visibility.Visible;
        showpass_btn.Visibility = Visibility.Collapsed;
    }


    private void NumericValidator(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9^.]");
        e.Handled = regex.IsMatch(e.Text);
    }
    
}