﻿namespace Fakrny.Domain.Dtos;

public class CourseGetDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Duration { get; set; }
    public LookupDto Author { get; set; } = null!;
}

public class CourseGetDetailsDto : CourseGetDto
{
    public string Description { get; set; } = null!;
    public IEnumerable<LookupDto>? Sections { get; set; }
}

public class CoursePostDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int AuthorId { get; set; }
}