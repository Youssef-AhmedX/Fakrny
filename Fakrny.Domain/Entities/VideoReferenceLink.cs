namespace Fakrny.Domain.Entities;
public class VideoReferenceLink
{
    public Video? Video { get; set; }
    public int VideoId { get; set; }

    public Language? ReferenceLink { get; set; }
    public int ReferenceLinkId { get; set; }
}
