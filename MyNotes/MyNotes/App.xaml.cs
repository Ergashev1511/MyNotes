using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyNotes.Data;
using MyNotes.Data.Repositories.IRepositories;
using MyNotes.Data.Repositories.Repositories;
using MyNotes.Services.IServices;
using MyNotes.Services.Services;

namespace MyNotes;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost AppHost { get; set; }

    private String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyNotes");


    public App()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
            
        if (!File.Exists(Variablies.StaticVariablies.DataBaseName))
        {
            File.Create(Variablies.StaticVariablies.DataBaseName); ;
        }


        using var dbcontext = new AppDbContext();             
        dbcontext.Database.Migrate();

        AppHost = Host.CreateDefaultBuilder().ConfigureServices((_, services) =>

            {
                services.AddDbContext<AppDbContext>();
                services.AddTransient<MainWindow>();

                services.AddTransient<IUserRepository, UserRepository>();
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<INotesRepository, NotesRepository>();
                services.AddTransient<INoteService, NoteService>();

            }
        ).Build();

    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();

        var startupform = AppHost.Services.GetRequiredService<MainWindow>();

        startupform.Show();
        base.OnStartup(e);

    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        base.OnExit(e);
    }
}