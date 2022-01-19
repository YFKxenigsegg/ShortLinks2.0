using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Domain.Entities;
using ShortLinks.Persistence;

namespace ShortLinks.Application.LinkManagment;
public class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public DeleteHandler(
        IDbContextFactory<ApplicationDbContext> dbContextFactory
        , IMapper mapper
        , IMediator mediator
        ) => (_dbContextFactory, _mapper, _mediator) = (dbContextFactory, mapper, mediator);

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken)
    {
        var entity = await _mediator.Send(_mapper.Map<GetRequest>(request), cancellationToken);
        //?? Exception pipeline

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        db.Set<Link>().Remove(_mapper.Map<Link>(entity));

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
