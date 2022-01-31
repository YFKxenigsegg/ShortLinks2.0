using ShortLinks.Domain.Entities;
using ShortLinks.Kernel.Interfaces;

namespace ShortLinks.Auth.Feature.Role.Models;
public class RoleInfo : IMapFrom<UserRole>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
}
