using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FluentValidation.AspNetCore;
using MediatR;
using NLog.Web;
using ShortLinks.Kernel;
using ShortLinks.Kernel.Converters;
using ShortLinks.Auth;
using ShortLinks.Kernel.Exceptions.Filter;
using ShortLinks.Persistence;
using NSwag.Generation.Processors.Security;
using NSwag;
using ShortLinks.Persistence.Migrations;

var _logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
_logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddKernel(builder.Configuration);
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddIdentity(builder.Configuration);
    builder.Services.AddMigrations(builder.Configuration);

    builder.Services.AddControllers(options =>
    options.Filters.Add(new ApiExceptionFilter()))
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        })
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName != null && x.FullName.Contains("ShortLinks2.0")).ToArray()));

    builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName != null && x.FullName.Contains("ShortLinks2.0")).ToArray());
    builder.Services.AddOpenApiDocument(config =>
    {
        config.Title = "ShortLinks2.0 Link Manager API";
        config.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Type into the textbox: Bearer {your JWT token}."
        });
        config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
    });
    builder.Services.AddMvcCore().AddApiExplorer();

    builder.Services.AddAutoMapper((serviceProvider, autoMapper) =>
    {
        autoMapper.AddCollectionMappers();
        autoMapper.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
    }, GetAutoMapperProfilesFromAllAssemblies().ToArray());


    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();
    app.UseIdentity();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.UseOpenApi();
    app.UseSwaggerUi3();
    app.UseRouting();

    app.Run();

    static IEnumerable<Type> GetAutoMapperProfilesFromAllAssemblies()
    {
        return from assembly in AppDomain.CurrentDomain.GetAssemblies()
               from aType in assembly.GetTypes()
               where aType.IsClass && !aType.IsAbstract && aType.IsSubclassOf(typeof(Profile))
               select aType;
    }
}
catch (Exception ex)
{
    _logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{

    NLog.LogManager.Shutdown();
}