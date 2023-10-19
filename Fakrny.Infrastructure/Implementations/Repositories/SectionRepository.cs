namespace Fakrny.Infrastructure.Implementations.Repositories;
public class SectionRepository : BaseRepository<Section>, ISectionRepository
{
    public SectionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Section> GetDetails(bool withNoTracking = true)
    {
        IQueryable<Section> query = _context.Set<Section>()
            .Include(S => S.Course)
            .Include(s => s.Videos);

        if (withNoTracking)
            query = query.AsNoTracking();

        return query;
    }

    public IEnumerable<Section> GetLookup(bool withNoTracking = true)
    {
        var query = _context.Set<Section>().Select(s => new Section { Id = s.Id, Name = s.Name });

        if (withNoTracking)
            query = query.AsNoTracking();

        return query.ToList();
    }
}
