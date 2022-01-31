using Microsoft.AspNetCore.Identity;

namespace ShortLinks.Domain.Entities;
public class UserRole : IdentityRole<int>
{
    public override int Id { get; set; }
    public override string Name { get; set; } = default!;
    public string Code { get; set; } = default!;

    public ICollection<UserLogin> UserLogin { get; set; } = default!;
}
