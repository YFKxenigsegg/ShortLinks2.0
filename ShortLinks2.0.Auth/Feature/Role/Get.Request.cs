using MediatR;
using ShortLinks.Auth.Feature.Role.Models;

namespace ShortLinks.Auth.Feature.Role;
public class GetRequest : IRequest<RoleInfo>
{
    public int? Id { get; set; }
    public string Code { get; set; } = default!;
}
