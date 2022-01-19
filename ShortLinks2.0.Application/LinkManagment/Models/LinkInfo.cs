using ShortLinks.Kernel.Interfaces;

namespace ShortLinks.Application.LinkManagment.Models;
public partial class LinkInfo : IMapFrom<LinkInfo>
{
    public int Id { get; set; }
    public string Link { get; set; } = default!;
    public DateOnly ExpirationDate { get; set; }
}
