namespace ShortLinks.Kernel.Models;
public class Error
{
    public string Message { get; set; } = default!;
    public string Description { get; set; } = default!;

    public Error() { }

    public Error(string message, string description) =>
        (Message, Description) = (message, description);

    public override string ToString() =>
        $"Message {Message}, Description {Description}";

}
