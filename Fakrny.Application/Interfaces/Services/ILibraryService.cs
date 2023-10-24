namespace Fakrny.Application.Interfaces.Services;
public interface ILibraryService : IBaseService<Library>
{
    IEnumerable<Library> GetAllWithDetails();
}
