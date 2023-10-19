namespace Fakrny.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
    private readonly IVideoService _videoService;
    private const string _entityName = "Video";

    public VideosController(IVideoService videoService)
    {
        _videoService = videoService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VideoDto>> GetAll()
    {
        var videos = _videoService.GetAll();

        var videosDto = new List<VideoDto>();

        foreach (var video in videos)
        {
            videosDto.Add(MapToDto(video));
        }

        return Ok(videosDto);
    }

    [HttpGet("{id}")]
    public ActionResult<VideoDto> GetById(int id)
    {
        var video = _videoService.GetById(id);

        if (video is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(video));
    }

    [HttpPost("Add")]
    public ActionResult<VideoDto> Add(VideoDto videoDto)
    {
        var video = MapToEntity(videoDto);

        _videoService.Add(video);

        return Ok(MapToDto(video));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<VideoDto> Update(VideoDto videoDto, int id)
    {
        var video = _videoService.GetById(id);

        if (video is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        video.Name = videoDto.Name;

        _videoService.Update(video);

        return Ok(MapToDto(video));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var video = _videoService.GetById(id);

        if (video is null)
            return BadRequest($"Can not find {_entityName} with this Id");

        video.IsDeleted = !video.IsDeleted;

        _videoService.Update(video);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {video.Id}");
    }

    private VideoDto MapToDto(Video video)
    {
        var videoDto = new VideoDto
        {
            Id = video.Id,
            Name = video.Name,
            IsDeleted = video.IsDeleted
        };

        return videoDto;
    }

    private Video MapToEntity(VideoDto videoDto)
    {
        var video = new Video
        {
            Id = videoDto.Id,
            Name = videoDto.Name,
            IsDeleted = videoDto.IsDeleted
        };

        return video;
    }
}
