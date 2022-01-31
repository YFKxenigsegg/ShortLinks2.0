using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Auth.Feature.User.Models;
using ShortLinks.Domain.Entities;
using ShortLinks.Kernel.Exceptions;
using ShortLinks.Persistence;

namespace ShortLinks.Auth.Feature.User;
public class GetHandler : IRequestHandler<GetRequest, UserInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public GetHandler(
        IDbContextFactory<ApplicationDbContext> dbContextFactory
        , IMapper mapper) => (_dbContextFactory, _mapper) = (dbContextFactory, mapper);

    public async Task<UserInfo> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<UserLogin>()
            .ProjectTo<UserInfo>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(request.Id == null
                ? $"Not found User/{request.UserId}" : $"Not found User/{request.Id}");
    }
}
