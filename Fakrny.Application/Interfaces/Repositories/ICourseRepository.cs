namespace Fakrny.Application.Interfaces.Repositories;
public interface ICourseRepository : IBaseRepository<Course>
{
    IQueryable<Course> GetDetails(bool withNoTracking = true);
}
