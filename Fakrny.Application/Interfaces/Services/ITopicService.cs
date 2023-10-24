namespace Fakrny.Application.Interfaces.Services;
public interface ITopicService : IBaseService<Topic>
{
    IEnumerable<Topic> GetAllWithDetails();
}
