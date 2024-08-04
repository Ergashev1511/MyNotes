using MyNotes.Models.Base;

namespace MyNotes.Models;

public class Tasks : BaseEntity
{
    public string NewTask { get; set; }
    public DateTime Day { get; set; }
    public DateTime Date { get; set; }
}