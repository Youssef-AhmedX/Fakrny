namespace Fakrny.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibrariesController : ControllerBase
{
    private readonly ILibraryService _libraryService;
    private const string _entityName = "Library";

    public LibrariesController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<LibraryDto>> GetAll()
    {
        var libraries = _libraryService.GetAll();

        var librariesDto = new List<LibraryDto>();

        foreach (var library in libraries)
        {
            librariesDto.Add(MapToDto(library));
        }

        return Ok(librariesDto);
    }

    [HttpGet("{id}")]
    public ActionResult<LibraryDto> GetById(int id)
    {
        var library = _libraryService.GetById(id);

        if (library is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(library));
    }

    [HttpPost("Add")]
    public ActionResult<LibraryDto> Add(LibraryDto libraryDto)
    {
        var library = MapToEntity(libraryDto);

        _libraryService.Add(library);

        return Ok(MapToDto(library));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<LibraryDto> Update(LibraryDto libraryDto, int id)
    {
        var library = _libraryService.GetById(id);

        if (library is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        library.Name = libraryDto.Name;

        _libraryService.Update(library);

        return Ok(MapToDto(library));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var library = _libraryService.GetById(id);

        if (library is null)
            return BadRequest($"Can not find {_entityName} with this Id");

        library.IsDeleted = !library.IsDeleted;

        _libraryService.Update(library);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {library.Id}");
    }

    private LibraryDto MapToDto(Library library)
    {
        var libraryDto = new LibraryDto
        {
            Id = library.Id,
            Name = library.Name,
            IsDeleted = library.IsDeleted
        };

        return libraryDto;
    }

    private Library MapToEntity(LibraryDto libraryDto)
    {
        var library = new Library
        {
            Id = libraryDto.Id,
            Name = libraryDto.Name,
            IsDeleted = libraryDto.IsDeleted
        };

        return library;
    }
}
