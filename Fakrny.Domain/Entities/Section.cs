namespace Fakrny.Domain.Entities;

[Index(nameof(Name), nameof(CourseId), IsUnique = true)]
public class Section : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Number { get; set; }
    public string Description { get; set; } = null!;
    public int? OrderIndex { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public ICollection<Video> Videos { get; set; } = new List<Video>();
}
