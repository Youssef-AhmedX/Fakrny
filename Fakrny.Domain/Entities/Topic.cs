namespace Fakrny.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Topic : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<VideoTopic> Videos { get; set; } = new List<VideoTopic>();
}
