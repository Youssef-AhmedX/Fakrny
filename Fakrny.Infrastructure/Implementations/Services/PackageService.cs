namespace Fakrny.Infrastructure.Implementations.Services;
public class PackageService : BaseService<Package>, IPackageService
{
    public PackageService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public IEnumerable<Package> GetAllWithDetails()
    {
        return _unitOfWork.Repository<Package>().GetQueryable()
            .Include(a => a.Videos).AsNoTracking().ToList();
    }
}
