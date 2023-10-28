namespace Fakrny.Application.Interfaces.Services;
public interface ISectionService : IBaseService<Section>
{
    Section? GetByIdWithDetails(int id);
}
