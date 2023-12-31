﻿namespace Fakrny.Blazor.Models.Dtos;
public class ReferenceLinkDto : BaseDto
{
    public int Id { get; set; }

    [Label("Link")]
    [Required]
    public string Link { get; set; } = null!;

    [Label("Website Name")]
    [Required]
    public string WebsiteName { get; set; } = null!;
    public int VideosCount { get; set; }

}
