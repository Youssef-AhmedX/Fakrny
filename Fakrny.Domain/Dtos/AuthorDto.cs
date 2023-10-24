namespace Fakrny.Domain.Dtos;
public class AuthorDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Nickname { get; set; }
    public int CoursesCount { get; set; }
}
