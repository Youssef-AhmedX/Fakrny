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
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IReferenceLinkService, ReferenceLinkService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<ISectionService, SectionService>();
        services.AddScoped<ICourseService, CourseService>();

        return services;
    }
}
