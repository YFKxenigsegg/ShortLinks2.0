using Microsoft.AspNetCore.Identity;

namespace ShortLinks.Auth.Identity;
public class KeyNormalizer : ILookupNormalizer
{
    public string NormalizeEmail(string email) => email;

    public string NormalizeName(string name) => name;

}
