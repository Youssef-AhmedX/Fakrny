namespace Fakrny.Infrastructure.Implementations.Services;
public class VideoService : BaseService<Video>, IVideoService
{
    public VideoService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Video? GetByIdWithDetails(int id)
    {
        return _unitOfWork.Repository<Video>().GetQueryable()
            .Include(v => v.Libraries).ThenInclude(l => l.Library)
            .Include(v => v.Packages).ThenInclude(p => p.Package)
            .Include(v => v.Topics).ThenInclude(t => t.Topic)
            .Include(v => v.Languages).ThenInclude(l => l.Language)
            .Include(v => v.ReferenceLinks).ThenInclude(r => r.ReferenceLink)
            .Include(v => v.Section).ThenInclude(s => s!.Course)
            .Select(v => new Video
            {
                Id = v.Id,
                Name = v.Name,
                Number = v.Number,
                DurationInMin = v.DurationInMin,
                OrderIndex = v.OrderIndex,
                Description = v.Description,
                Section = new Section
                {
                    Id = v.Id,
                    Name = v.Section!.Name,
                    Number = v.Section.Number,
                    Course = new Course
                    {
                        Id = v.Section.Course!.Id,
                        Name = v.Section.Course.Name
                    },
                },
                ReferenceLinks = v.ReferenceLinks,
                Languages = v.Languages,
                Topics = v.Topics,
                Packages = v.Packages,
                Libraries = v.Libraries
            }).AsNoTracking().FirstOrDefault(c => c.Id == id);
    }
}
