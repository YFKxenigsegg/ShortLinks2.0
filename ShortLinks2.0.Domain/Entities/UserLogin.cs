using Microsoft.AspNetCore.Identity;

namespace ShortLinks.Domain.Entities;
public class UserLogin : IdentityUser<int>
{
    public override int Id { get; set; }
    public string UserId { get; set; } = default!;
    public override string PasswordHash { get; set; } = default!;
    public DateTime CreateTime { get; set; }
    public DateTime? LastLoginTime { get; set; }
    public int UserRoleId { get; set; }
    public override string Email { get; set; } = default!;

    public virtual UserRole UserRole { get; set; } = default!;
}
