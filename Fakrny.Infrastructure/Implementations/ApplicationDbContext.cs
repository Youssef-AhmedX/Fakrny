namespace Fakrny.Infrastructure.Implementations;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<ReferenceLink> ReferenceLinks { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<VideoLanguage> VideoLanguages { get; set; }
    public DbSet<VideoLibrary> VideoLibraries { get; set; }
    public DbSet<VideoPackage> VideoPackages { get; set; }
    public DbSet<VideoReferenceLink> VideoReferenceLinks { get; set; }
    public DbSet<VideoTopic> VideoTopics { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<VideoLanguage>().HasKey(e => new { e.VideoId, e.LanguageId });
        builder.Entity<VideoTopic>().HasKey(e => new { e.VideoId, e.TopicId });
        builder.Entity<VideoLibrary>().HasKey(e => new { e.VideoId, e.LibraryId });
        builder.Entity<VideoPackage>().HasKey(e => new { e.VideoId, e.PackageId });
        builder.Entity<VideoReferenceLink>().HasKey(e => new { e.VideoId, e.ReferenceLinkId });

        base.OnModelCreating(builder);
    }
}
