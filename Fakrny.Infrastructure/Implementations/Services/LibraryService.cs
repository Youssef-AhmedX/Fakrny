namespace Fakrny.Infrastructure.Implementations.Services;
public class LibraryService : BaseService<Library>, ILibraryService
{
    public LibraryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public IEnumerable<Library> GetAllWithDetails()
    {
        return _unitOfWork.Repository<Library>().GetQueryable()
            .Include(a => a.Videos).AsNoTracking().ToList();
    }
}
