namespace Fakrny.Infrastructure.Implementations.Services;
public class ReferenceLinkService : BaseService<ReferenceLink>, IReferenceLinkService
{
    public ReferenceLinkService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
