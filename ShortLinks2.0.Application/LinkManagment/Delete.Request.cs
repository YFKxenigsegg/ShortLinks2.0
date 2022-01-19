using MediatR;

namespace ShortLinks.Application.LinkManagment;
public class DeleteRequest : IRequest<Unit>
{
    public int Id { get; set; }
}
