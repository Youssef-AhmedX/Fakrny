namespace Fakrny.Blazor.Pages.Sections;

public partial class SectionDetails
{
    [Parameter] public int Id { get; set; }

    private SectionDetailsDto? sectionDto;

    protected override async Task OnParametersSetAsync()
    {
        sectionDto = await GetByIdAsync($"Sections/WithDetails/{Id}");

        if (sectionDto is not null)
            breadcrumbItems.AddRange(new List<BreadcrumbItem>
            {
                new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
                new BreadcrumbItem("Courses", href: "/Courses", icon: Icons.Material.Filled.OndemandVideo),
                new BreadcrumbItem(sectionDto.Course.Name, href: $"/Courses/Details/{sectionDto.Course.Id}"),
                new BreadcrumbItem($"{sectionDto.Number}. {sectionDto.Name}", href: null,disabled: true),
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

        DialogParameters<SectionForm> formParameters = new()
        {
            { x => x.Id, id },
            { x => x.CourseId, sectionDto!.Course.Id },
        };

        var dialog = await DialogService.ShowAsync<SectionForm>("Edit Section", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        SectionDetailsDto sectionDetails = (SectionDetailsDto)result.Data;

        sectionDto!.Name = sectionDetails.Name;
        sectionDto!.Number = sectionDetails.Number;
        sectionDto!.OrderIndex = sectionDetails.OrderIndex;
        sectionDto.Description = sectionDetails.Description;
        sectionDto.IsDeleted = sectionDetails.IsDeleted;
    }

    private void ChangeDuration((int Duration, bool IsAdded) args)
    {
        if (args.IsAdded)
            sectionDto!.DurationInMin += args.Duration;
        else
            sectionDto!.DurationInMin -= args.Duration;
    }
}
