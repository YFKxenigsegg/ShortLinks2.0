using MediatR;
using ShortLinks.Application.LinkManagment.Models;

namespace ShortLinks.Application.LinkManagment;
public class GetRequest : IRequest<LinkInfo>
{
    public int Id { get; set; }
}
