namespace Fakrny.Domain.Entities;

[Index(nameof(Name), nameof(SectionId), IsUnique = true)]
public class Video : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DurationInMin { get; set; }
    public int? OrderIndex { get; set; }

    public int SectionId { get; set; }
    public Section? Section { get; set; }

    public ICollection<VideoTopic> Topics { get; set; } = new List<VideoTopic>();
    public ICollection<VideoLanguage> Languages { get; set; } = new List<VideoLanguage>();
    public ICollection<VideoPackage> Packages { get; set; } = new List<VideoPackage>();
    public ICollection<VideoLibrary> Libraries { get; set; } = new List<VideoLibrary>();
    public ICollection<VideoReferenceLink> ReferenceLinks { get; set; } = new List<VideoReferenceLink>();
}
