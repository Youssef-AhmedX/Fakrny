namespace Fakrny.Application.Interfaces.Services;
public interface IVideoService : IBaseService<Video>
{
    Video? GetByIdWithDetails(int id);
    Video? GetVideoById(int id);
}
