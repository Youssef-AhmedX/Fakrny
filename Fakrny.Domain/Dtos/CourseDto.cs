namespace Fakrny.Domain.Dtos;

public class CourseDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int DurationInMin { get; set; }
    public bool IsPaid { get; set; }
    public LookupDto Author { get; set; } = null!;
}

public class CourseDetailsDto : CourseDto
{
    public string Description { get; set; } = null!;
    public IEnumerable<SectionDto>? Sections { get; set; }
}

public class CoursePostDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsPaid { get; set; }
    public int AuthorId { get; set; }
}