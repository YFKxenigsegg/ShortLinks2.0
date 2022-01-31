using MediatR;
using ShortLinks.Auth.Feature.User.Models;

namespace ShortLinks.Auth.Feature.User;
public class GetRequest : IRequest<UserInfo>
{
    public int? Id { get; set; }
    public string? UserId { get; set; }
    public string? Email { get; set; }
}
