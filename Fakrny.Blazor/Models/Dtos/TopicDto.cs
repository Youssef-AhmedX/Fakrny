namespace Fakrny.Blazor.Models.Dtos;
public class TopicDto : BaseDto
{
    public int Id { get; set; }

    [Label("Topic Name")]
    [Required]
    public string Name { get; set; } = null!;
    public int VideosCount { get; set; }

}
