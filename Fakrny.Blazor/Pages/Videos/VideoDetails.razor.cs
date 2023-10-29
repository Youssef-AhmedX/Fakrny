namespace Fakrny.Blazor.Pages.Videos;

public partial class VideoDetails
{
    [Parameter] public int Id { get; set; }

    private VideoDetailsDto? videoDto;

    protected override async Task OnParametersSetAsync()
    {
        videoDto = await GetByIdAsync($"Videos/WithDetails/{Id}");
        if (videoDto is not null)
            breadcrumbItems.AddRange(new List<BreadcrumbItem>
            {
                new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
                new BreadcrumbItem("Courses", href: "/Courses", icon: Icons.Material.Filled.OndemandVideo),
                new BreadcrumbItem(videoDto.Course.Name, href: $"/Courses/Details/{videoDto.Course.Id}"),
                new BreadcrumbItem($"{videoDto.Section.Number}. {videoDto.Section.Name}", href: $"/Sections/Details/{videoDto.Section.Id}"),
                new BreadcrumbItem($"{videoDto.Number}. {videoDto.Name}", href: null,disabled: true),
            });
    }

    private async Task OpenForm(int id)
    {
        DialogOptions dialogOptions = new()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
            Position = DialogPosition.Center,
            CloseButton = true
        };

        DialogParameters<VideoForm> formParameters = new()
        {
            { x => x.Id, id },
            { x => x.SectionId, videoDto!.Section.Id },
        };

        var dialog = await DialogService.ShowAsync<VideoForm>("Edit Video", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        VideoDetailsDto videoDetails = (VideoDetailsDto)result.Data;

        videoDto!.Name = videoDetails.Name;
        videoDto!.Number = videoDetails.Number;
        videoDto!.OrderIndex = videoDetails.OrderIndex;
        videoDto.Description = videoDetails.Description;
        videoDto.IsDeleted = videoDetails.IsDeleted;
        videoDto.Packages = videoDetails.Packages;
        videoDto.Libraries = videoDetails.Libraries;
        videoDto.Topics = videoDetails.Topics;
        videoDto.Languages = videoDetails.Languages;
        videoDto.ReferenceLinks = videoDetails.ReferenceLinks;
    }
}
