namespace Fakrny.Infrastructure.Implementations.Repositories;
public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Course> GetDetails(bool withNoTracking = true)
    {
        IQueryable<Course> query = _context.Set<Course>()
            .Include(S => S.Author)
            .Include(s => s.Sections);

        if (withNoTracking)
            query = query.AsNoTracking();

        return query;
    }
}
