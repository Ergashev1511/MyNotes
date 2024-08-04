namespace MyNotes.Models.Base;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime IsCreate { get; set; }=DateTime.Now;
}