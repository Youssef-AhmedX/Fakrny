namespace Fakrny.Domain.Entities;
public class VideoLibrary
{
    public Video? Video { get; set; }
    public int VideoId { get; set; }

    public Library? Library { get; set; }
    public int LibraryId { get; set; }
}
