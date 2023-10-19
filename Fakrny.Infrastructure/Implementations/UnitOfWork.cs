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

    public ISectionRepository SectionRepository()
    {
        var repository = new SectionRepository(_context);

        return repository;
    }
    public IVideoRepository VideoRepository()
    {
        var repository = new VideoRepository(_context);

        return repository;
    }
    public ICourseRepository CourseRepository()
    {
        var repository = new CourseRepository(_context);

        return repository;
    }

}
