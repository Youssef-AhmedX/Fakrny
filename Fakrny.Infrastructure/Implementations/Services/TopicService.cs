namespace Fakrny.Infrastructure.Implementations.Services;
public class TopicService : BaseService<Topic>, ITopicService
{
    public TopicService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

}
