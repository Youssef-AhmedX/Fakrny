namespace Fakrny.Infrastructure.Implementations.Services;
public class SectionService : BaseService<Section>, ISectionService
{
    public SectionService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
