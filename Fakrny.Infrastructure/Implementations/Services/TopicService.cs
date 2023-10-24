namespace Fakrny.Infrastructure.Implementations.Services;
public class TopicService : BaseService<Topic>, ITopicService
{
    public TopicService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public IEnumerable<Topic> GetAllWithDetails()
    {
        return _unitOfWork.Repository<Topic>().GetQueryable()
            .Include(a => a.Videos).AsNoTracking().ToList();
    }

}
