namespace Fakrny.Blazor.Models.Dtos;
public class PackageDto : BaseDto
{
    public int Id { get; set; }

    [Label("Package Name")]
    [Required]
    public string Name { get; set; } = null!;
}
