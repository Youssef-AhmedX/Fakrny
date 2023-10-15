namespace Fakrny.Infrastructure.Implementations.Services;
public class LanguageService : BaseService<Language>, ILanguageService
{
    public LanguageService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
