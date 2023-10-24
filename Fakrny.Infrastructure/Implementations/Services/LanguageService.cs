namespace Fakrny.Infrastructure.Implementations.Services;
public class LanguageService : BaseService<Language>, ILanguageService
{
    public LanguageService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public IEnumerable<Language> GetAllWithDetails()
    {
        return _unitOfWork.Repository<Language>().GetQueryable()
            .Include(a => a.Videos).AsNoTracking().ToList();
    }
}
