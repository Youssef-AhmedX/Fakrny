namespace Fakrny.Domain.Dtos;

public class CourseDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

}

public class CourseGetDto : CourseDto
{
    public LookupDto Author { get; set; } = null!;
    public int SectionsCount { get; set; }
    public int VideosCount { get; set; }
}

public class CourseGetDetailsDto : CourseDto
{
    public string Description { get; set; } = null!;
    public LookupDto Author { get; set; } = null!;
    public IEnumerable<LookupDto>? Sections { get; set; }
}

public class CoursePostDto : CourseDto
{
    public string Description { get; set; } = null!;
    public int AuthorId { get; set; }
}