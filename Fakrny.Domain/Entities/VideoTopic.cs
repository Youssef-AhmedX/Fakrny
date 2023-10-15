namespace Fakrny.Domain.Entities;
public class VideoTopic
{
    public Video? Video { get; set; }
    public int VideoId { get; set; }

    public Topic? Topic { get; set; }
    public int TopicId { get; set; }
}
