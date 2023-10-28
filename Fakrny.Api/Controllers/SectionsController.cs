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

    [HttpGet("WithDetails/{id}")]
    public ActionResult<SectionDetailsDto> GetByIdWithDetails(int id)
    {
        var section = _sectionService.GetByIdWithDetails(id);

        if (section is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        var SectionDto = new SectionDetailsDto
        {
            Id = section.Id,
            Name = section.Name,
            Number = section.Number,
            OrderIndex = section.OrderIndex,
            Description = section.Description,
            IsDeleted = section.IsDeleted,
            DurationInMin = section.Videos.Where(v => !v.IsDeleted).Sum(v => v.DurationInMin),
            Course = new LookupDto
            {
                Id = section.CourseId,
                Name = section.Course!.Name
            },
            Videos = section.Videos.Select(s => new VideoDto
            {
                Id = s.Id,
                Name = s.Name,
                Number = s.Number,
                OrderIndex = s.OrderIndex,
                IsDeleted = s.IsDeleted,
                DurationInMin = s.DurationInMin
            }),
        };

        return Ok(SectionDto);
    }

    [HttpGet("{id}")]
    public ActionResult<SectionPostDto> GetById(int id)
    {
        var section = _sectionService.GetById(id);

        if (section is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(section));
    }

    [HttpPost("Add")]
    public ActionResult<SectionPostDto> Add(SectionPostDto sectionDto)
    {
        var section = new Section
        {
            Id = sectionDto.Id,
            Name = sectionDto.Name,
            Number = sectionDto.Number,
            OrderIndex = sectionDto.OrderIndex,
            Description = sectionDto.Description,
            CourseId = sectionDto.CourseId,
            IsDeleted = sectionDto.IsDeleted,
        };

        _sectionService.Add(section);

        return Ok(MapToDto(section));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<SectionPostDto> Update(SectionPostDto sectionDto, int id)
    {
        var section = _sectionService.GetById(id);

        if (section is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        section.Name = sectionDto.Name;
        section.Number = sectionDto.Number;
        section.OrderIndex = sectionDto.OrderIndex;
        section.Description = sectionDto.Description;
        section.CourseId = sectionDto.CourseId;
        section.IsDeleted = sectionDto.IsDeleted;

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

    private SectionPostDto MapToDto(Section section)
    {
        var sectionDto = new SectionPostDto
        {
            Id = section.Id,
            Name = section.Name,
            Number = section.Number,
            OrderIndex = section.OrderIndex,
            Description = section.Description,
            CourseId = section.CourseId,
            IsDeleted = section.IsDeleted
        };

        return sectionDto;
    }
}
