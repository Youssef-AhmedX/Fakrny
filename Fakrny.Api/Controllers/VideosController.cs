﻿namespace Fakrny.Api.Controllers;
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

    [HttpGet("WithDetails/{id}")]
    public ActionResult<VideoDetailsDto> GetByIdWithDetails(int id)
    {
        var video = _videoService.GetByIdWithDetails(id);

        if (video is null)
            return NotFound($"Can not find {_entityName} with Id equal {id}");

        var videoDto = new VideoDetailsDto
        {
            Id = video.Id,
            Name = video.Name,
            Number = video.Number,
            OrderIndex = video.OrderIndex,
            Description = video.Description,
            IsDeleted = video.IsDeleted,
            DurationInMin = video.DurationInMin,
            Section = new SectionDto
            {
                Id = video.Section!.Id,
                Name = video.Section.Name,
                Number = video.Section.Number,
            },
            Course = new LookupDto
            {
                Id = video.Section.Course!.Id,
                Name = video.Section.Course.Name,
            },
            ReferenceLinks = video.ReferenceLinks.Select(r => new LookupDto() { Id = r.ReferenceLink!.Id, Name = r.ReferenceLink.WebsiteName }),
            Topics = video.Topics.Select(t => new LookupDto() { Id = t.Topic!.Id, Name = t.Topic.Name }),
            Packages = video.Packages.Select(p => new LookupDto() { Id = p.Package!.Id, Name = p.Package.Name }),
            Libraries = video.Libraries.Select(l => new LookupDto() { Id = l.Library!.Id, Name = l.Library.Name }),
            Languages = video.Languages.Select(l => new LookupDto() { Id = l.Language!.Id, Name = l.Language.Name })
        };

        return Ok(videoDto);
    }

    [HttpGet("{id}")]
    public ActionResult<VideoPostDto> GetById(int id)
    {
        var video = _videoService.GetVideoById(id);

        if (video is null)
            return NotFound($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(video));
    }

    [HttpPost("Add")]
    public ActionResult<VideoPostDto> Add(VideoPostDto videoDto)
    {
        var video = new Video
        {
            Name = videoDto.Name,
            Number = videoDto.Number,
            DurationInMin = videoDto.DurationInMin,
            OrderIndex = videoDto.OrderIndex,
            Description = videoDto.Description,
            IsDeleted = videoDto.IsDeleted,
            SectionId = videoDto.SectionId,
            Topics = videoDto.TopicsId?.Select(Id => new VideoTopic { TopicId = Id }).ToList()!,
            Packages = videoDto.PackagesId?.Select(Id => new VideoPackage { PackageId = Id }).ToList()!,
            Libraries = videoDto.LibrariesId?.Select(Id => new VideoLibrary { LibraryId = Id }).ToList()!,
            Languages = videoDto.LanguagesId?.Select(Id => new VideoLanguage { LanguageId = Id }).ToList()!,
            ReferenceLinks = videoDto.ReferenceLinksId?.Select(Id => new VideoReferenceLink { ReferenceLinkId = Id }).ToList()!,
        };

        _videoService.Add(video);

        return Ok(MapToDto(video));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<VideoPostDto> Update(VideoPostDto videoDto, int id)
    {
        var video = _videoService.GetVideoById(id);

        if (video is null)
            return NotFound($"Can not find {_entityName} with Id equal {id}");

        video.Name = videoDto.Name;
        video.Number = videoDto.Number;
        video.DurationInMin = videoDto.DurationInMin;
        video.OrderIndex = videoDto.OrderIndex;
        video.Description = videoDto.Description;
        video.IsDeleted = videoDto.IsDeleted;
        video.Topics = videoDto.TopicsId?.Select(Id => new VideoTopic { TopicId = Id }).ToList()!;
        video.Packages = videoDto.PackagesId?.Select(Id => new VideoPackage { PackageId = Id }).ToList()!;
        video.Libraries = videoDto.LibrariesId?.Select(Id => new VideoLibrary { LibraryId = Id }).ToList()!;
        video.Languages = videoDto.LanguagesId?.Select(Id => new VideoLanguage { LanguageId = Id }).ToList()!;
        video.ReferenceLinks = videoDto.ReferenceLinksId?.Select(Id => new VideoReferenceLink { ReferenceLinkId = Id }).ToList()!;

        _videoService.Update(video);

        return Ok(MapToDto(video));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var video = _videoService.GetById(id);

        if (video is null)
            return NotFound($"Can not find {_entityName} with this Id");

        video.IsDeleted = !video.IsDeleted;

        _videoService.Update(video);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {video.Id}");
    }

    private VideoPostDto MapToDto(Video video)
    {
        var videoDto = new VideoPostDto
        {
            Id = video.Id,
            Name = video.Name,
            Number = video.Number,
            Description = video.Description,
            DurationInMin = video.DurationInMin,
            OrderIndex = video.OrderIndex,
            SectionId = video.SectionId,
            IsDeleted = video.IsDeleted,
            ReferenceLinksId = video.ReferenceLinks?.Select(r => r.ReferenceLinkId)!,
            TopicsId = video.Topics?.Select(r => r.TopicId)!,
            PackagesId = video.Packages?.Select(r => r.PackageId)!,
            LibrariesId = video.Libraries?.Select(r => r.LibraryId)!,
            LanguagesId = video.Languages?.Select(r => r.LanguageId)!,
        };

        return videoDto;
    }
}
