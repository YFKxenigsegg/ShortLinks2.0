using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ShortLinks.Auth.Feature.Role.Models;
using ShortLinks.Auth.Feature.User.Models;
using ShortLinks.Auth.Identity;
using ShortLinks.Kernel.Models;
using ShortLinks.Kernel.Options;
using System.Net;

namespace ShortLinks.Auth;
public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptionsSection = configuration.GetSection("AuthOptions");

        services.AddOptions();
        services.Configure<AuthOptions>(authOptionsSection);

        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IPasswordHasher<UserInfo>, CustomPasswordHasher>();
        services.AddScoped<ILookupNormalizer, KeyNormalizer>();

        services.AddIdentity<UserInfo, RoleInfo>(options =>
        {
            // Basic built in validations
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 12;

            options.Lockout.MaxFailedAccessAttempts = 5;
        })
        //.AddEntityFrameworkStores<ApplicationDbContext>()
        ;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                var authOptons = authOptionsSection.Get<AuthOptions>();
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptons.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptons.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = authOptons.GetSymmetricSecutiryKey(),
                    ValidateIssuerSigningKey = true,

                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        context.Response
                            .WriteAsync(JsonConvert.SerializeObject(new Error("error", "Token expired. Please, authorize.")))
                            .Wait();

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddTransient<IUserStore<UserInfo>, UserStore>();
        services.AddTransient<IRoleStore<RoleInfo>, RoleStore>();

        return services;
    }

    public static void UseIdentity(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
