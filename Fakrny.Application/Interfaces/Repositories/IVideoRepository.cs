namespace Fakrny.Application.Interfaces.Repositories;
public interface IVideoRepository : IBaseRepository<Video>
{
    IQueryable<Video> GetDetails(bool withNoTracking = true);
}
