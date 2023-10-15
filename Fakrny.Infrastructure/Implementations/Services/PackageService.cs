namespace Fakrny.Infrastructure.Implementations.Services;
public class PackageService : BaseService<Package>, IPackageService
{
    public PackageService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
