namespace Fakrny.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController : ControllerBase
{
    private readonly ILanguageService _languageService;
    private const string _entityName = "Language";

    public LanguagesController(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<LanguageDto>> GetAll()
    {
        var languages = _languageService.GetAllWithDetails();

        var languagesDto = new List<LanguageDto>();

        foreach (var language in languages)
        {
            languagesDto.Add(MapToDto(language));
        }

        return Ok(languagesDto);
    }

    [HttpGet("{id}")]
    public ActionResult<LanguageDto> GetById(int id)
    {
        var language = _languageService.GetById(id);

        if (language is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(language));
    }

    [HttpPost("Add")]
    public ActionResult<LanguageDto> Add(LanguageDto languageDto)
    {
        var language = MapToEntity(languageDto);

        _languageService.Add(language);

        return Ok(MapToDto(language));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<LanguageDto> Update(LanguageDto languageDto, int id)
    {
        var language = _languageService.GetById(id);

        if (language is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        language.Name = languageDto.Name;

        _languageService.Update(language);

        return Ok(MapToDto(language));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var language = _languageService.GetById(id);

        if (language is null)
            return BadRequest($"Can not find {_entityName} with this Id");

        language.IsDeleted = !language.IsDeleted;

        _languageService.Update(language);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {language.Id}");
    }

    private LanguageDto MapToDto(Language language)
    {
        var languageDto = new LanguageDto
        {
            Id = language.Id,
            Name = language.Name,
            IsDeleted = language.IsDeleted,
            VideosCount = language.Videos.Count,
        };

        return languageDto;
    }

    private Language MapToEntity(LanguageDto languageDto)
    {
        var language = new Language
        {
            Id = languageDto.Id,
            Name = languageDto.Name,
            IsDeleted = languageDto.IsDeleted
        };

        return language;
    }
}
