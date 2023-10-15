namespace Fakrny.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Library : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<VideoLibrary> Videos { get; set; } = new List<VideoLibrary>();
}
