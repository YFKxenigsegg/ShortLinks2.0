using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Domain.Entities;
using ShortLinks.Persistence;

namespace ShortLinks.Application.LinkManagment;
public class UpdateHandler : IRequestHandler<UpdateRequest, LinkInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateHandler(
        IDbContextFactory<ApplicationDbContext> dbContextFactory
        , IMapper mapper
        , IMediator mediator
        ) => (_dbContextFactory, _mapper, _mediator) = (dbContextFactory, mapper, mediator);

    //TODO: turn ON IDENTITY-INSERT for not existing enity
    public async Task<LinkInfo> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        //var entity = await _mediator.Send(_mapper.Map<GetRequest>(request), cancellationToken);
        //?? Exception pipeline

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await db.Set<Link>()
            .Persist(_mapper)
            .InsertOrUpdateAsync(_mapper.Map<LinkInfo>(request), cancellationToken);

        await db.SaveChangesAsync(cancellationToken);


        return await _mediator.Send(_mapper.Map<GetRequest>(request), cancellationToken);
    }
}
