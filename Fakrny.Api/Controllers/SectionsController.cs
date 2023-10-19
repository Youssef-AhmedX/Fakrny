namespace Fakrny.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SectionsController : ControllerBase
{
    private readonly ISectionService _sectionService;
    private const string _entityName = "Section";

    public SectionsController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SectionDto>> GetAll()
    {
        var sections = _sectionService.GetAll();

        var sectionsDto = new List<SectionDto>();

        foreach (var section in sections)
        {
            sectionsDto.Add(MapToDto(section));
        }

        return Ok(sectionsDto);
    }

    [HttpGet("{id}")]
    public ActionResult<SectionDto> GetById(int id)
    {
        var section = _sectionService.GetById(id);

        if (section is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(section));
    }

    [HttpPost("Add")]
    public ActionResult<SectionDto> Add(SectionDto sectionDto)
    {
        var section = MapToEntity(sectionDto);

        _sectionService.Add(section);

        return Ok(MapToDto(section));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<SectionDto> Update(SectionDto sectionDto, int id)
    {
        var section = _sectionService.GetById(id);

        if (section is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        section.Name = sectionDto.Name;

        _sectionService.Update(section);

        return Ok(MapToDto(section));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var section = _sectionService.GetById(id);

        if (section is null)
            return BadRequest($"Can not find {_entityName} with this Id");

        section.IsDeleted = !section.IsDeleted;

        _sectionService.Update(section);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {section.Id}");
    }

    private SectionDto MapToDto(Section section)
    {
        var sectionDto = new SectionDto
        {
            Id = section.Id,
            Name = section.Name,
            IsDeleted = section.IsDeleted
        };

        return sectionDto;
    }

    private Section MapToEntity(SectionDto sectionDto)
    {
        var section = new Section
        {
            Id = sectionDto.Id,
            Name = sectionDto.Name,
            IsDeleted = sectionDto.IsDeleted
        };

        return section;
    }
}
