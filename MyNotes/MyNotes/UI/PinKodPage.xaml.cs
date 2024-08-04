using System.Windows;
using System.Windows.Controls;
using MyNotes.Services.IServices;

namespace MyNotes.UI;

public partial class PinKodPage : UserControl
{
    private MainWindow _mainWindow;
    private IUserService _userService;
    public PinKodPage()
    {
        InitializeComponent();
    }

    public void SetMainWindow(MainWindow mainWindow,IUserService userService)
    {
        _mainWindow = mainWindow;
        _userService = userService;
    }

    private async void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (PasswordBox.Password.Length == 6)
        {
          
            if (await _userService.LoginByPin(PasswordBox.Password))
            {
                _mainWindow.Pin_doc.Visibility = Visibility.Collapsed;
                _mainWindow.Main_doc.Visibility = Visibility.Visible;
                PasswordBox.Password = string.Empty;
            }
            else
            {
                MessageBox.Show("InCorrect password!");
                PasswordBox.Password = string.Empty;
            }
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;
        PasswordBox.Password = PasswordBox.Password + button.Content;
    }

    private void Back_btn_OnClick(object sender, RoutedEventArgs e)
    {
        PasswordBox.Password = PasswordBox.Password.Substring(0, PasswordBox.Password.Length - 1);
    }
}