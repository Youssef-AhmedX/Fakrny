namespace Fakrny.Infrastructure.Implementations.Services;
public class SectionService : BaseService<Section>, ISectionService
{
    public SectionService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Section? GetByIdWithDetails(int id)
    {
        return _unitOfWork.SectionRepository().GetDetails().FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<Section> GetAllWithDetails()
    {
        return _unitOfWork.SectionRepository().GetDetails().ToList();
    }
}
