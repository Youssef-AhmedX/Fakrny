namespace Fakrny.Infrastructure.Implementations.Services;
public class VideoService : BaseService<Video>, IVideoService
{
    public VideoService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
