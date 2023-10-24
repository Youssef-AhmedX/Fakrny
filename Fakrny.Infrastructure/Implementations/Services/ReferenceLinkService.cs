namespace Fakrny.Infrastructure.Implementations.Services;
public class ReferenceLinkService : BaseService<ReferenceLink>, IReferenceLinkService
{
    public ReferenceLinkService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public IEnumerable<ReferenceLink> GetAllWithDetails()
    {
        return _unitOfWork.Repository<ReferenceLink>().GetQueryable()
            .Include(a => a.Videos).AsNoTracking().ToList();
    }
}
