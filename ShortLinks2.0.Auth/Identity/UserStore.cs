using AutoMapper;
using MediatR;
using AutoMapper.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShortLinks.Auth.Feature.User;
using ShortLinks.Auth.Feature.User.Models;
using ShortLinks.Persistence;
using ShortLinks.Kernel.Exceptions;
using ShortLinks.Domain.Entities;

namespace ShortLinks.Auth.Identity;
public class UserStore :
    IUserStore<UserInfo>,
    IUserPasswordStore<UserInfo>,
    IUserLockoutStore<UserInfo>,
    IUserRoleStore<UserInfo>,
    IUserEmailStore<UserInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserStore(
        IDbContextFactory<ApplicationDbContext> dbContextFactory
        , IMapper mapper
        , IMediator mediator
        ) => (_dbContextFactory, _mapper, _mediator) = (dbContextFactory, mapper, mediator);

    public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await db.Set<UserInfo>()
            .Persist(_mapper)
            .InsertOrUpdateAsync(user, cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await db.Set<UserInfo>()
            .Persist(_mapper)
            .InsertOrUpdateAsync(user, cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (userId == null) throw new ArgumentNullException(nameof(userId));

        try
        {
            return await _mediator.Send(new GetRequest { UserId = userId }, cancellationToken);
        }
        catch (NotFoundException)
        {
            return default!;
        }
    }

    public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<UserLogin>()
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"Not found User/{user.UserId}");

        return entity.PasswordHash;
    }

    public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<UserLogin>()
           .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"Not found User/{user.UserId}");

        return !string.IsNullOrWhiteSpace(entity.PasswordHash);
    }

    public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<UserLogin>()
           .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"Not found User/{user.UserId}");

        entity.PasswordHash = passwordHash;
    }

    public void Dispose() { }


    //TODO: complete other methods (attention on model)
    public Task AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken) =>
            throw new NotImplementedException();

    public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<UserInfo> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<UserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<int> GetAccessFailedCountAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<string> GetEmailAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<bool> GetEmailConfirmedAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<bool> GetLockoutEnabledAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<DateTimeOffset?> GetLockoutEndDateAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<string> GetNormalizedEmailAsync(UserInfo user, CancellationToken cancellationToken) =>
            throw new NotImplementedException();

    public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<IList<UserInfo>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<int> IncrementAccessFailedCountAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<bool> IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task ResetAccessFailedCountAsync(UserInfo user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetEmailAsync(UserInfo user, string email, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetEmailConfirmedAsync(UserInfo user, bool confirmed, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetLockoutEnabledAsync(UserInfo user, bool enabled, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetLockoutEndDateAsync(UserInfo user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetNormalizedEmailAsync(UserInfo user, string normalizedEmail, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
