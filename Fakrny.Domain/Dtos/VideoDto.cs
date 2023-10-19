namespace Fakrny.Domain.Dtos;
public class VideoDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DurationInMin { get; set; }
    public int OrderIndex { get; set; }
    public LookupDto Section { get; set; } = null!;
    public IEnumerable<LookupDto>? ReferenceLinks { get; set; }
    public IEnumerable<LookupDto>? Libraries { get; set; }
    public IEnumerable<LookupDto>? Packages { get; set; }
    public IEnumerable<LookupDto>? Topics { get; set; }
    public IEnumerable<LookupDto>? Languages { get; set; }

}
