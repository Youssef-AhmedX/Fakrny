namespace Fakrny.Blazor.Models.Dtos;

public class CourseDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public LookupDto Author { get; set; } = null!;
    public int DurationInMin { get; set; }
    public bool IsPaid { get; set; }
}

public class CourseGetDetailsDto : CourseDto
{
    public string Description { get; set; } = null!;
    public List<SectionDto> Sections { get; set; } = new List<SectionDto>();
}

public class CoursePostDto : BaseDto
{
    public int Id { get; set; }
    [Required]
    [Label("Course Name")]
    public string Name { get; set; } = null!;
    [Required]
    [Label("Course Description")]
    public string Description { get; set; } = null!;
    [Required]
    [Label("Course Author")]
    public int? AuthorId { get; set; }
    public bool IsPaid { get; set; }

}