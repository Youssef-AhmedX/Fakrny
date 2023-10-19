namespace Fakrny.Infrastructure.Implementations.Services;
public class AuthorService : BaseService<Author>, IAuthorService
{
    public AuthorService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
