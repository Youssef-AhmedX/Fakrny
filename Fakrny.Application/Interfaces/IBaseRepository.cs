namespace Fakrny.Application.Interfaces;
public interface IBaseRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll(bool withNoTracking = true);
    TEntity? GetById(int id);
    IQueryable<TEntity> GetQueryable();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
