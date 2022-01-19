namespace ShortLinks.Domain.Entities;
public class Link
{
    public int LinkId { get; set; }
    public string LinkValue { get; set; } = default!;
    public string OriginalLink { get; set; } = default!;
    public DateOnly Created { get; set; }
    public DateOnly ExpirationDate { get; set; }

    public User User { get; set; } = default!;
}
