using System.ComponentModel.DataAnnotations;

namespace Fakrny.Blazor.Dtos;

public class AuthorDto : BaseDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

}
