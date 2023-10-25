namespace Fakrny.Domain.Entities;

[Index(nameof(Name), nameof(AuthorId), IsUnique = true)]
public class Course : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsPaid { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
