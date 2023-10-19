namespace Fakrny.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private const string _entityName = "Author";

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AuthorDto>> GetAll()
    {
        var authors = _authorService.GetAll();

        var authorsDto = new List<AuthorDto>();

        foreach (var author in authors)
        {
            authorsDto.Add(MapToDto(author));
        }

        return Ok(authorsDto);
    }

    [HttpGet("{id}")]
    public ActionResult<AuthorDto> GetById(int id)
    {
        var author = _authorService.GetById(id);

        if (author is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(author));
    }

    [HttpPost("Add")]
    public ActionResult<AuthorDto> Add(AuthorDto authorDto)
    {
        var author = MapToEntity(authorDto);

        _authorService.Add(author);

        return Ok(MapToDto(author));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<AuthorDto> Update(AuthorDto authorDto, int id)
    {
        var author = _authorService.GetById(id);

        if (author is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        author.Name = authorDto.Name;

        _authorService.Update(author);

        return Ok(MapToDto(author));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var author = _authorService.GetById(id);

        if (author is null)
            return BadRequest($"Can not find {_entityName} with this Id");

        author.IsDeleted = !author.IsDeleted;

        _authorService.Update(author);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {author.Id}");
    }

    private AuthorDto MapToDto(Author author)
    {
        var authorDto = new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            IsDeleted = author.IsDeleted
        };

        return authorDto;
    }

    private Author MapToEntity(AuthorDto authorDto)
    {
        var author = new Author
        {
            Id = authorDto.Id,
            Name = authorDto.Name,
            IsDeleted = authorDto.IsDeleted
        };

        return author;
    }
}
