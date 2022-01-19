using MediatR;
using ShortLinks.Application.LinkManagment.Models;

namespace ShortLinks.Application.LinkManagment;
public class UpdateRequest : LinkInfo, IRequest<LinkInfo> { }
