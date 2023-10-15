namespace Fakrny.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Language : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<VideoLanguage> Videos { get; set; } = new List<VideoLanguage>();
}
