using Newtonsoft.Json;

namespace ShortLinks.Auth.Feature.User.Models;
public partial class UserInfo
{
    [JsonIgnore]
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string? Password { get; set; }
    public string Email { get; set; } = default!;
    public DateTime? CreateTime { get; set; }
    public DateTime? LastLoginTime { get; set; }
    [JsonIgnore]
    public int TotalSuccessLoginCount { get; set; }


    //public string Role { get; set; } = default!;
    //public bool IsTempPassword { get; set; }
}
