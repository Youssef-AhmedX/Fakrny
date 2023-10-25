namespace Fakrny.Blazor.Models.Dtos;

public class CourseDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public LookupDto Author { get; set; } = null!;
    public int Duration { get; set; }

}

public class CourseGetDetailsDto : CourseDto
{
    public string Description { get; set; } = null!;
    public IEnumerable<LookupDto>? Sections { get; set; }
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
}