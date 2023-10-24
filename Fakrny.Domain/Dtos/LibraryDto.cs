namespace Fakrny.Domain.Dtos;
public class LibraryDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int VideosCount { get; set; }
}
