namespace Fakrny.Blazor.Models.Dtos;
public class LanguageDto : BaseDto
{
    public int Id { get; set; }

    [Label("Language Name")]
    [Required]
    public string Name { get; set; } = null!;
    public int VideosCount { get; set; }

}
