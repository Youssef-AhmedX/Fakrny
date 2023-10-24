namespace Fakrny.Application.Interfaces.Services;
public interface IAuthorService : IBaseService<Author>
{
    IEnumerable<Author> GetAllWithDetails();
}
