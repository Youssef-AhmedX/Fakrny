namespace Fakrny.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly ITopicService _topicService;
    private const string _entityName = "Topic";

    public TopicsController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TopicDto>> GetAll()
    {
        var topics = _topicService.GetAllWithDetails();

        var topicsDto = new List<TopicDto>();

        foreach (var topic in topics)
        {
            topicsDto.Add(MapToDto(topic));
        }

        return Ok(topicsDto);
    }

    [HttpGet("{id}")]
    public ActionResult<TopicDto> GetById(int id)
    {
        var topic = _topicService.GetById(id);

        if (topic is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(topic));
    }

    [HttpPost("Add")]
    public ActionResult<TopicDto> Add(TopicDto topicDto)
    {
        var topic = MapToEntity(topicDto);

        _topicService.Add(topic);

        return Ok(MapToDto(topic));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<TopicDto> Update(TopicDto topicDto, int id)
    {
        var topic = _topicService.GetById(id);

        if (topic is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        topic.Name = topicDto.Name;

        _topicService.Update(topic);

        return Ok(MapToDto(topic));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var topic = _topicService.GetById(id);

        if (topic is null)
            return BadRequest($"Can not find {_entityName} with this Id");

        topic.IsDeleted = !topic.IsDeleted;

        _topicService.Update(topic);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {topic.Id}");
    }

    private TopicDto MapToDto(Topic topic)
    {
        var topicDto = new TopicDto
        {
            Id = topic.Id,
            Name = topic.Name,
            IsDeleted = topic.IsDeleted,
            VideosCount = topic.Videos.Count,
        };

        return topicDto;
    }

    private Topic MapToEntity(TopicDto topicDto)
    {
        var topic = new Topic
        {
            Id = topicDto.Id,
            Name = topicDto.Name,
            IsDeleted = topicDto.IsDeleted
        };

        return topic;
    }
}
