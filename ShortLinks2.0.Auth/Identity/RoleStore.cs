using MediatR;
using Microsoft.AspNetCore.Identity;
using ShortLinks.Auth.Feature.Role;
using ShortLinks.Auth.Feature.Role.Models;

namespace ShortLinks.Auth.Identity;
public class RoleStore : IRoleStore<RoleInfo>
{
    private readonly IMediator _mediator;

    public RoleStore(IMediator mediator) => _mediator = mediator;

    public async Task<RoleInfo> FindByIdAsync(int roleId, CancellationToken cancellationToken) =>
        await _mediator.Send(new GetRequest { Id = roleId }, cancellationToken);

    public async Task<RoleInfo> FindByNameAsync(string roleName, CancellationToken cancellationToken) =>
        await _mediator.Send(new GetRequest { Code = roleName }, cancellationToken);

    public void Dispose() { }


    public Task<IdentityResult> CreateAsync(RoleInfo role, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<IdentityResult> DeleteAsync(RoleInfo role, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<RoleInfo> FindByIdAsync(string roleId, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<string> GetNormalizedRoleNameAsync(RoleInfo role, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<string> GetRoleIdAsync(RoleInfo role, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<string> GetRoleNameAsync(RoleInfo role, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetNormalizedRoleNameAsync(RoleInfo role, string normalizedName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task SetRoleNameAsync(RoleInfo role, string roleName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<IdentityResult> UpdateAsync(RoleInfo role, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
