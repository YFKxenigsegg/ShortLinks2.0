using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Domain.Entities;
using ShortLinks.Persistence;

namespace ShortLinks.Application.LinkManagment;
public class GetHandler : IRequestHandler<GetRequest, LinkInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public GetHandler(
        IDbContextFactory<ApplicationDbContext> dbContextFactory
        , IMapper mapper
        ) => (_dbContextFactory, _mapper) = (dbContextFactory, mapper);

    public async Task<LinkInfo> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        await using var db = _dbContextFactory.CreateDbContext();

        return await db.Set<Link>()
            .AsNoTracking()
            .Where(x => x.LinkId == request.Id)
            .ProjectTo<LinkInfo>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ?? default!;
    }
}
