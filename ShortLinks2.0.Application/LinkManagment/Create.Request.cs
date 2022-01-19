using MediatR;
using ShortLinks.Application.LinkManagment.Models;

namespace ShortLinks.Application.LinkManagment;
public class CreateRequest : IRequest<LinkInfo>
{
    public string OriginalLink { get; set; } = default!;
}
