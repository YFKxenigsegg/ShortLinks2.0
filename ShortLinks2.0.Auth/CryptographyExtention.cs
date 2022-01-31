namespace ShortLinks.Auth;
public class CryptographyExtention
{
    public static string CreateHash(string input)
    {
        const int _workFactor = 12;

        var hash = BCrypt.Net.BCrypt.HashPassword(input, _workFactor);

        return hash;
    }

    public static bool Verify(string input, string hash) =>
        BCrypt.Net.BCrypt.Verify(input, hash);
}
