namespace Fakrny.Infrastructure.Implementations;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var repository = new BaseRepository<TEntity>(_context);

        return repository;
    }
}
