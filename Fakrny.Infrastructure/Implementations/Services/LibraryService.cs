namespace Fakrny.Infrastructure.Implementations.Services;
public class LibraryService : BaseService<Library>, ILibraryService
{
    public LibraryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
