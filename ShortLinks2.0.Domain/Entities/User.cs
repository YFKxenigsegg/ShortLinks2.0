namespace ShortLinks.Domain.Entities;
public class User
{
    public int UserId { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    
    //PasswordCode
    // [NotMapped] Token
}
