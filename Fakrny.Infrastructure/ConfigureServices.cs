namespace Fakrny.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IPackageService, PackageService>();
        services.AddScoped<ILibraryService, LibraryService>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IReferenceLinkService, ReferenceLinkService>();

        return services;
    }
}
