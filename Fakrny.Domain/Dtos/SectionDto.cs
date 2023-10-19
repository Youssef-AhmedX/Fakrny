namespace Fakrny.Domain.Dtos;
public class SectionDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int OrderIndex { get; set; }
    public LookupDto Course { get; set; } = null!;
    public IEnumerable<LookupDto>? Videos { get; set; }
}
