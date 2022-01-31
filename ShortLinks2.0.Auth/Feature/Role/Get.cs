using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Auth.Feature.Role.Models;
using ShortLinks.Domain.Entities;
using ShortLinks.Kernel.Exceptions;
using ShortLinks.Persistence;

namespace ShortLinks.Auth.Feature.Role;
public class GetHandler : IRequestHandler<GetRequest, RoleInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public GetHandler(
        IDbContextFactory<ApplicationDbContext> dbContextFactory
        , IMapper mapper
        ) => (_dbContextFactory, _mapper) = (dbContextFactory, mapper);

    public async Task<RoleInfo> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<UserRole>()
            .ProjectTo<RoleInfo>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(request.Id == null 
                ? $"Not found Role/{request.Code}" : $"Not fouund Role/{request.Id}");
    }
}
