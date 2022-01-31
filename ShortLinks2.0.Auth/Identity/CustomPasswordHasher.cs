using Microsoft.AspNetCore.Identity;
using ShortLinks.Auth.Feature.User;
using ShortLinks.Auth.Feature.User.Models;

namespace ShortLinks.Auth.Identity;
public class CustomPasswordHasher : PasswordHasher<UserInfo>
{
    private const string _salt = "D98EE44D-DCCE-496E-AAC6-178DA9CC3FA6";

    public override string HashPassword(UserInfo user, string password) =>
        CryptographyExtention.CreateHash(password + _salt);

    public override PasswordVerificationResult VerifyHashedPassword(UserInfo user, string hashedPassword, string providedPassword) =>
        CryptographyExtention.Verify(providedPassword + _salt, hashedPassword)
            ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
}
