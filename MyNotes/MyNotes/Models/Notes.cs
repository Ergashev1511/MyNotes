using MyNotes.Models.Base;

namespace MyNotes.Models;

public class Notes : BaseEntity
{
    public string Title { get; set; }
    public string Note { get; set; }
}