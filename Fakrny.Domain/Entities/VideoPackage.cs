namespace Fakrny.Domain.Entities;
public class VideoPackage
{
    public Video? Video { get; set; }
    public int VideoId { get; set; }

    public Package? Package { get; set; }
    public int PackageId { get; set; }
}
