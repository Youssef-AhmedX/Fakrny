namespace Fakrny.Domain.Dtos;
public class ReferenceLinkDto : BaseDto
{
    public int Id { get; set; }
    public string Link { get; set; } = null!;
    public string WebsiteName { get; set; } = null!;
    public int VideosCount { get; set; }
}
