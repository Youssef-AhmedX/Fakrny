namespace Fakrny.Blazor.Models.Dtos;

public class AuthorDto : BaseDto
{
    public int Id { get; set; }

    [Label("Author Name")]
    [Required]
    public string Name { get; set; } = null!;

    [Label("Author Nickname")]
    public string? Nickname { get; set; }

    public int CoursesCount { get; set; }

}
