namespace Fakrny.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Author : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Nickname { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
