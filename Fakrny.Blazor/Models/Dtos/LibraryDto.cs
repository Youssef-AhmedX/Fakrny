namespace Fakrny.Blazor.Models.Dtos;
public class LibraryDto : BaseDto
{
    public int Id { get; set; }

    [Label("Library Name")]
    [Required]
    public string Name { get; set; } = null!;
    public int VideosCount { get; set; }

}
