using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Kernel.Extensions;
using ShortLinks.Kernel.Pagination;
using ShortLinks.Persistence;

namespace ShortLinks.Application.LinkManagment;
public class GetAllHandler : IRequestHandler<GetAllRequest, IPagedList<LinkInfo>>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public GetAllHandler(
        IDbContextFactory<ApplicationDbContext> dbContextFactory
        , IMapper mapper
        ) => (_dbContextFactory, _mapper) = (dbContextFactory, mapper);

    public async Task<IPagedList<LinkInfo>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return db.Links.ProjectTo<LinkInfo>(_mapper.ConfigurationProvider)
            .ApplyPaging(request);
    }
}
