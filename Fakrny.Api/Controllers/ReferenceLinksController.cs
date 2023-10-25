namespace Fakrny.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReferenceLinksController : ControllerBase
{
    private readonly IReferenceLinkService _referenceLinkService;
    private const string _entityName = "ReferenceLink";

    public ReferenceLinksController(IReferenceLinkService referenceLinkService)
    {
        _referenceLinkService = referenceLinkService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReferenceLinkDto>> GetAll()
    {
        var referenceLinks = _referenceLinkService.GetAllWithDetails();

        var referenceLinksDto = new List<ReferenceLinkDto>();

        foreach (var referenceLink in referenceLinks)
        {
            referenceLinksDto.Add(MapToDto(referenceLink));
        }

        return Ok(referenceLinksDto);
    }

    [HttpGet("{id}")]
    public ActionResult<ReferenceLinkDto> GetById(int id)
    {
        var referenceLink = _referenceLinkService.GetById(id);

        if (referenceLink is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(referenceLink));
    }

    [HttpPost("Add")]
    public ActionResult<ReferenceLinkDto> Add(ReferenceLinkDto referenceLinkDto)
    {
        var referenceLink = MapToEntity(referenceLinkDto);

        _referenceLinkService.Add(referenceLink);

        return Ok(MapToDto(referenceLink));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<ReferenceLinkDto> Update(ReferenceLinkDto referenceLinkDto, int id)
    {
        var referenceLink = _referenceLinkService.GetById(id);

        if (referenceLink is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        referenceLink.Link = referenceLinkDto.Link;
        referenceLink.WebsiteName = referenceLinkDto.WebsiteName;
        referenceLink.IsDeleted = referenceLinkDto.IsDeleted;

        _referenceLinkService.Update(referenceLink);

        return Ok(MapToDto(referenceLink));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var referenceLink = _referenceLinkService.GetById(id);

        if (referenceLink is null)
            return BadRequest($"Can not find {_entityName} with this Id");

        referenceLink.IsDeleted = !referenceLink.IsDeleted;

        _referenceLinkService.Update(referenceLink);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {referenceLink.Id}");
    }

    private ReferenceLinkDto MapToDto(ReferenceLink referenceLink)
    {
        var referenceLinkDto = new ReferenceLinkDto
        {
            Id = referenceLink.Id,
            Link = referenceLink.Link,
            WebsiteName = referenceLink.WebsiteName,
            IsDeleted = referenceLink.IsDeleted,
            VideosCount = referenceLink.Videos.Count,
        };

        return referenceLinkDto;
    }

    private ReferenceLink MapToEntity(ReferenceLinkDto referenceLinkDto)
    {
        var referenceLink = new ReferenceLink
        {
            Id = referenceLinkDto.Id,
            Link = referenceLinkDto.Link,
            WebsiteName = referenceLinkDto.WebsiteName,
            IsDeleted = referenceLinkDto.IsDeleted
        };

        return referenceLink;
    }
}
