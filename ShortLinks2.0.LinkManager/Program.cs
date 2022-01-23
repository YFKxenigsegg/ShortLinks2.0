using AutoMapper;
using AutoMapper.EquivalencyExpression;
using MediatR;
using ShortLinks.Kernel.Converters;
using ShortLinks.Kernel.Exceptions.Filter;
using ShortLinks.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

//TODO: add logging

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers(options =>
options.Filters.Add(new ApiExceptionFilter()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
    });

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()
        .Where(x => x.FullName != null && x.FullName.Contains("ShortLinks2.0")).ToArray());
builder.Services.AddOpenApiDocument(config => config.Title = "ShortLinks2.0 Link Manager API");
builder.Services.AddMvcCore().AddApiExplorer(); //

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseOpenApi();
app.UseSwaggerUi3();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

static IEnumerable<Type> GetAutoMapperProfilesFromAllAssemblies() //access modifier?
{
    return from assembly in AppDomain.CurrentDomain.GetAssemblies()
           from aType in assembly.GetTypes()
           where aType.IsClass && !aType.IsAbstract && aType.IsSubclassOf(typeof(Profile))
           select aType;
}
