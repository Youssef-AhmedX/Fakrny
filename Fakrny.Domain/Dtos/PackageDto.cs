namespace Fakrny.Domain.Dtos;
public class PackageDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int VideosCount { get; set; }
}
