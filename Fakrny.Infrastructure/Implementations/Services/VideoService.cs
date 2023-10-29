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
                    Id = v.Section!.Id,
                    Name = v.Section.Name,
                    Number = v.Section.Number,
                    Course = new Course
                    {
                        Id = v.Section.Course!.Id,
                        Name = v.Section.Course.Name
                    },
                },
                ReferenceLinks = v.ReferenceLinks.Select(r => new VideoReferenceLink() { ReferenceLinkId = r.ReferenceLinkId, ReferenceLink = new ReferenceLink { Id = r.ReferenceLink!.Id, Link = r.ReferenceLink.Link, WebsiteName = r.ReferenceLink.WebsiteName } }).ToList(),
                Languages = v.Languages.Select(l => new VideoLanguage() { LanguageId = l.LanguageId, Language = new Language { Id = l.Language!.Id, Name = l.Language.Name } }).ToList(),
                Topics = v.Topics.Select(t => new VideoTopic() { TopicId = t.TopicId, Topic = new Topic { Id = t.Topic!.Id, Name = t.Topic.Name } }).ToList(),
                Packages = v.Packages.Select(p => new VideoPackage() { PackageId = p.PackageId, Package = new Package { Id = p.Package!.Id, Name = p.Package.Name } }).ToList(),
                Libraries = v.Libraries.Select(l => new VideoLibrary() { LibraryId = l.LibraryId, Library = new Library { Id = l.Library!.Id, Name = l.Library.Name } }).ToList(),
            }).AsNoTracking().FirstOrDefault(c => c.Id == id);
    }

    public Video? GetVideoById(int id)
    {
        return _unitOfWork.Repository<Video>().GetQueryable()
            .Include(v => v.Libraries).Include(v => v.Packages)
            .Include(v => v.Topics).Include(v => v.Languages)
            .Include(v => v.ReferenceLinks).FirstOrDefault(c => c.Id == id);
    }
}
