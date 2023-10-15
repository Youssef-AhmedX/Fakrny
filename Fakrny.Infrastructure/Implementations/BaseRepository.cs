namespace Fakrny.Infrastructure.Implementations.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TEntity> GetAll(bool withNoTracking = true)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (withNoTracking)
            query = query.AsNoTracking();

        return query.ToList();
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>();
    }

    public TEntity? GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public void Add(TEntity entity)
    {
        _context.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Remove(entity);
    }
}
