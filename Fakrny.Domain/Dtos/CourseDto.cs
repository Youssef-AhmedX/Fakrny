namespace Fakrny.Domain.Dtos;
public class CourseDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public AuthorDto Author { get; set; } = null!;
    public IEnumerable<LookupDto>? Sections { get; set; }
}
