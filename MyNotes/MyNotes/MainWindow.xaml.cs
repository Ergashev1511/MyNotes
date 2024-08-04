using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyNotes.Services.IServices;
using MyNotes.Services.Services;
using MyNotes.UI.Compenanta;

namespace MyNotes;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IUserService _userService;
    private INoteService _noteService;
    public MainWindow(IUserService userService,INoteService noteService)
    {
        _userService = userService;
        _noteService = noteService;
        
        InitializeComponent();
        signUp.SetMainWindow(this,userService);
        Pinkod.SetMainWindow(this,userService);
        main_page.SetMainWindow(this,noteService);
    }

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (await _userService.CheckForEntry())
        {
            Pin_doc.Visibility = Visibility.Visible;
            SignUp_doc.Visibility = Visibility.Collapsed;
        }
        else
        {
            Pin_doc.Visibility = Visibility.Collapsed;
            SignUp_doc.Visibility = Visibility.Visible;
        }
        
    }
}