namespace Fakrny.Domain.Entities;

[Index(nameof(Link), IsUnique = true)]
public class ReferenceLink : BaseEntity
{
    public int Id { get; set; }
    public string Link { get; set; } = null!;

    public ICollection<VideoReferenceLink> Videos { get; set; } = new List<VideoReferenceLink>();
}
