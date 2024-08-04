using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MyNotes.Data.Repositories.IRepositories;
using MyNotes.Models;
using MyNotes.Services.IServices;
using MyNotes.UI.Compenanta;

namespace MyNotes.UI;

public partial class MainPage : UserControl
{
    private MainWindow _mainWindow;
    private INoteService _noteService;
    private List<Notes> _notesList = new List<Notes>();
 
    public MainPage()
    {
        InitializeComponent();
    }

    public void SetMainWindow(MainWindow mainWindow,INoteService noteService)
    {
        _mainWindow = mainWindow;
        _noteService = noteService;
       GetAllNotes();
    }

    public async void GetAllNotes()
    {
        _notesList = await _noteService.GetAll();
        if (_notesList != null && _notesList.Any())
        {
            GetAllNotesForList();
        }
    }
    

    public void GetAllNotesForList()
    {
        main_wrap.Visibility = Visibility.Visible;
        main_wrap.Children.Clear();
        foreach (var i in _notesList)
        {
            NotesFor notesFor = new NotesFor();
            notesFor.Tag = i.Id;
            notesFor.SetNotesName(i.Title,i.IsCreate.ToString());
            notesFor.Margin = new Thickness(5,5,5,5);
            main_wrap.Children.Add(notesFor);
            notesFor.SetMainMainPage(this,_noteService);
        }
    }
    private void AddNote_btn_OnClick(object sender, RoutedEventArgs e)
    {
        notesList_Dcc.Visibility = Visibility.Collapsed;
        AddNotes_Doc.Visibility = Visibility.Visible;
    }

    private void Back_btn_OnClick(object sender, RoutedEventArgs e)
    {
        notesList_Dcc.Visibility = Visibility.Visible;
        AddNotes_Doc.Visibility = Visibility.Collapsed;
    }

    private void Note_btn_OnClick(object sender, RoutedEventArgs e)
    {
        notesList_Dcc.Visibility = Visibility.Visible;
    }

    private async void Save_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var newnote = new Notes();
        if (title_txt.Text == string.Empty || note_txt.Text==string.Empty)
        {
            MessageBox.Show("Iltimos maydonlarni to'ldiring!");
        }

        newnote.Title = title_txt.Text;
        newnote.Note = note_txt.Text;

        await _noteService.AddNotes(newnote);
        title_txt.Text = note_txt.Text = string.Empty;
        GetAllNotes();
        AddNotes_Doc.Visibility = Visibility.Collapsed;
        notesList_Dcc.Visibility = Visibility.Visible;
    }
}