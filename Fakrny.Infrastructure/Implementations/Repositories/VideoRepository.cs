namespace Fakrny.Infrastructure.Implementations.Repositories;
public class VideoRepository : BaseRepository<Video>, IVideoRepository
{
    public VideoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Video> GetDetails(bool withNoTracking = true)
    {
        IQueryable<Video> query = _context.Set<Video>()
            .Include(S => S.Section)
            .Include(s => s.Libraries)
            .ThenInclude(l => l.Library)
            .Include(s => s.Packages)
            .ThenInclude(l => l.Package)
            .Include(s => s.ReferenceLinks)
            .ThenInclude(l => l.ReferenceLink)
            .Include(s => s.Topics)
            .ThenInclude(l => l.Topic);

        if (withNoTracking)
            query = query.AsNoTracking();

        return query;
    }
}
