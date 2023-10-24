namespace Fakrny.Application.Interfaces.Services;
public interface IPackageService : IBaseService<Package>
{
    IEnumerable<Package> GetAllWithDetails();
}
