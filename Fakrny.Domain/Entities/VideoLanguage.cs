namespace Fakrny.Domain.Entities;
public class VideoLanguage
{
    public int LanguageId { get; set; }
    public Language? Language { get; set; }

    public int VideoId { get; set; }
    public Video? Video { get; set; }
}
