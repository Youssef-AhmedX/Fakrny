namespace Fakrny.Application.Interfaces.Repositories;
public interface ISectionRepository : IBaseRepository<Section>
{
    IQueryable<Section> GetDetails(bool withNoTracking = true);
}
