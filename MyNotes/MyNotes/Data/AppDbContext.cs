using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MyNotes.Models;

namespace MyNotes.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Notes> Notes { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    private SqliteConnection connectiion;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        connectiion ??= InitiializeSqliteConnection();

        optionsBuilder.UseSqlite(connectiion);

        base.OnConfiguring(optionsBuilder);
    }

    private static SqliteConnection InitiializeSqliteConnection()
    {
        var connectionsttring = new SqliteConnectionStringBuilder()
        {
            DataSource = Variablies.StaticVariablies.DataBaseName,
        };
        return new SqliteConnection(connectionsttring.ToString());
            
    }
}