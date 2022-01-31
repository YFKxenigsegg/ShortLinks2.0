using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ShortLinks.Kernel.Options;
public class AuthOptions
{
    public string AppPrefix { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Key { get; set; } = default!;
    public int AccessTokenExpireTimeSpanInMinutes { get; set; }
    public int RefreshTokenExpiretimeSpanInMinutes { get; set; }
    public bool AllowInsecureTokenRequest { get; set; }
    public bool WindowsAuthEnabled { get; set; }
    public string WindowsAuthServerUrl { get; set; } = default!;

    public SymmetricSecurityKey GetSymmetricSecutiryKey() =>
          new(Encoding.ASCII.GetBytes(Key));

}
