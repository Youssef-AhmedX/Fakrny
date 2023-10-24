namespace Fakrny.Application.Interfaces.Services;
public interface ILanguageService : IBaseService<Language>
{
    IEnumerable<Language> GetAllWithDetails();
}
