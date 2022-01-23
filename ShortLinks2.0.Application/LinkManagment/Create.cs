using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Domain.Entities;
using ShortLinks.Persistence;

namespace ShortLinks.Application.LinkManagment;
public class CreateHandler : IRequestHandler<CreateRequest, LinkInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateHandler(
        IDbContextFactory<ApplicationDbContext> dbContextFactory
        , IMediator mediator
        , IMapper mapper
        ) => (_dbContextFactory, _mapper, _mediator) = (dbContextFactory, mapper, mediator);

    public async Task<LinkInfo> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var link = _mapper.Map<Link>(request);

        await db.Set<Link>().AddAsync(link, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(_mapper.Map<GetRequest>(link.LinkId), cancellationToken);
    }
}
