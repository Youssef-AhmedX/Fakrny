namespace Fakrny.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PackagesController : ControllerBase
{
    private readonly IPackageService _packageService;
    private const string _entityName = "Package";

    public PackagesController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PackageDto>> GetAll()
    {
        var packages = _packageService.GetAllWithDetails();

        var packagesDto = new List<PackageDto>();

        foreach (var package in packages)
        {
            packagesDto.Add(MapToDto(package));
        }

        return Ok(packagesDto);
    }

    [HttpGet("{id}")]
    public ActionResult<PackageDto> GetById(int id)
    {
        var package = _packageService.GetById(id);

        if (package is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(package));
    }

    [HttpPost("Add")]
    public ActionResult<PackageDto> Add(PackageDto packageDto)
    {
        var package = MapToEntity(packageDto);

        _packageService.Add(package);

        return Ok(MapToDto(package));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<PackageDto> Update(PackageDto packageDto, int id)
    {
        var package = _packageService.GetById(id);

        if (package is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        package.Name = packageDto.Name;
        package.IsDeleted = packageDto.IsDeleted;

        _packageService.Update(package);

        return Ok(MapToDto(package));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var package = _packageService.GetById(id);

        if (package is null)
            return BadRequest($"Can not find {_entityName} with this Id");

        package.IsDeleted = !package.IsDeleted;

        _packageService.Update(package);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {package.Id}");
    }

    private PackageDto MapToDto(Package package)
    {
        var packageDto = new PackageDto
        {
            Id = package.Id,
            Name = package.Name,
            IsDeleted = package.IsDeleted,
            VideosCount = package.Videos.Count,
        };

        return packageDto;
    }

    private Package MapToEntity(PackageDto packageDto)
    {
        var package = new Package
        {
            Id = packageDto.Id,
            Name = packageDto.Name,
            IsDeleted = packageDto.IsDeleted
        };

        return package;
    }
}
