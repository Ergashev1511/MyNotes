using System.Windows;
using System.Windows.Controls;
using MyNotes.Models;
using MyNotes.Models.Base;
using MyNotes.Services.IServices;

namespace MyNotes.UI.Compenanta;

public partial class NotesFor : UserControl
{
    private INoteService _noteService;
    private MainPage _mainPage;
    public NotesFor()
    {
        InitializeComponent();
    }

    public void SetMainMainPage(MainPage mainPage,INoteService noteService)
    {
        // buni cherez nimadir orqali bog'lash kerak.
        _noteService = noteService;
        _mainPage = mainPage;
    }
    
    private async void Note_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            long noteId = (long)this.Tag;
            var notes = await _noteService.GetById(noteId);
            _mainPage.title_txt.Text = notes.Title;
            _mainPage.note_txt.Text = notes.Note;
            _mainPage.AddNotes_Doc.Visibility = Visibility.Visible;
            _mainPage.notesList_Dcc.Visibility = Visibility.Collapsed;
            IsVisible(true);
            _mainPage.save_btn.Visibility = Visibility.Collapsed;
        }
        catch (Exception exception)
        {
            MessageBox.Show("404   Not found!");
        }
    }

    public void SetNotesName(string title,  string day)
    {
       title_txt.Text=title;
        day_txt.Text=day;
    }
                              // Update qismi ishlamadi faqat!!!!
    private async void Update_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            long noteId = (long)this.Tag;
            var notes = await _noteService.GetById(noteId);
            _mainPage.title_txt.Text = notes.Title;
            _mainPage.note_txt.Text = notes.Note;
            
            
            var newNote = new Notes();
            newNote.Title = _mainPage.title_txt.Text;
            newNote.Note = _mainPage.note_txt.Text;
            IsVisible(false);
            _mainPage.Visibility = Visibility.Visible;
            if (newNote.Title != string.Empty && newNote.Note != string.Empty)
            {
                await _noteService.Update(newNote);
            }

            _mainPage.title_txt.Text = _mainPage.note_txt.Text = string.Empty;
            _mainPage.GetAllNotes();
            _mainPage.notesList_Dcc.Visibility = Visibility.Visible;
            _mainPage.AddNotes_Doc.Visibility = Visibility.Collapsed;
        }
        catch (Exception exception)
        {
            MessageBox.Show("404   Not found!");
        }
    }

    private async void Delete_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            long noteId = (long)this.Tag;
            var notes = await _noteService.GetById(noteId);
            var messageBoxResult = MessageBox.Show("Do you want to delete!","Ogohlantirish!", MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                await _noteService.Delete(notes);
                _mainPage.GetAllNotes();
                _mainPage.AddNotes_Doc.Visibility = Visibility.Collapsed;
                _mainPage.notesList_Dcc.Visibility = Visibility.Visible;
            }
            
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    public void IsVisible(bool isread)
    {
        _mainPage.title_txt.IsReadOnly = isread;
        _mainPage.note_txt.IsReadOnly = isread;
    }
}