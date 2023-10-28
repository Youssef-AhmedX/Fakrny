namespace Fakrny.Blazor.Models.Dtos;

public class SectionDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Number { get; set; } = null!;
    public int DurationInMin { get; set; }
    public int? OrderIndex { get; set; }
}

public class SectionDetailsDto : SectionDto
{
    public LookupDto Course { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<VideoDto> Videos { get; set; } = new List<VideoDto>();
}

public class SectionPostDto : BaseDto
{
    public int Id { get; set; }
    [Required]
    [Label("Section Name")]
    public string Name { get; set; } = null!;
    [Required]
    [Label("Section Number")]
    public string Number { get; set; } = null!;
    [Label("Section Index")]
    public int? OrderIndex { get; set; }
    [Required]
    [Label("Course Description")]
    public string Description { get; set; } = null!;
    public int CourseId { get; set; }
}