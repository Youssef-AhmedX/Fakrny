﻿namespace Fakrny.Blazor.Models.Dtos;

public class VideoDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Number { get; set; } = null!;
    public int DurationInMin { get; set; }
    public int? OrderIndex { get; set; }
}

public class VideoDetailsDto : VideoDto
{
    public SectionDto Section { get; set; } = null!;
    public LookupDto Course { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<LookupDto> ReferenceLinks { get; set; } = new List<LookupDto>();
    public IEnumerable<LookupDto> Libraries { get; set; } = new List<LookupDto>();
    public IEnumerable<LookupDto> Packages { get; set; } = new List<LookupDto>();
    public IEnumerable<LookupDto> Topics { get; set; } = new List<LookupDto>();
    public IEnumerable<LookupDto> Languages { get; set; } = new List<LookupDto>();
}

public class VideoPostDto : BaseDto
{
    public int Id { get; set; }
    [Required]
    [Label("Video Name")]
    public string Name { get; set; } = null!;
    [Required]
    [Label("Video Number")]
    public string Number { get; set; } = null!;
    [Label("Video Index")]
    public int? OrderIndex { get; set; }
    [Required]
    [Label("Video Description")]
    public string Description { get; set; } = null!;
    [Required]
    [Label("Video Duration In Min")]
    public int DurationInMin { get; set; }
    public int? SectionId { get; set; }
    public IEnumerable<int> ReferenceLinksId { get; set; } = new List<int>();
    public IEnumerable<int> LibrariesId { get; set; } = new List<int>();
    public IEnumerable<int> PackagesId { get; set; } = new List<int>();
    public IEnumerable<int> TopicsId { get; set; } = new List<int>();
    public IEnumerable<int> LanguagesId { get; set; } = new List<int>();
}
