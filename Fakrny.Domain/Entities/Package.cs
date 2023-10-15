namespace Fakrny.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Package : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<VideoPackage> Videos { get; set; } = new List<VideoPackage>();
}
