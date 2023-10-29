namespace Fakrny.Domain.Dtos;
public class SectionDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Number { get; set; }
    public int DurationInMin { get; set; }
    public int? OrderIndex { get; set; }
}

public class SectionDetailsDto : SectionDto
{
    public LookupDto Course { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<VideoDto>? Videos { get; set; }
}

public class SectionPostDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Number { get; set; }
    public int? OrderIndex { get; set; }
    public string Description { get; set; } = null!;
    public int CourseId { get; set; }
}
