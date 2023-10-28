namespace Fakrny.Blazor.Pages.Courses;

public partial class CourseDetails
{
    [Parameter] public int Id { get; set; }

    private CourseGetDetailsDto? courseDto;

    protected override async Task OnParametersSetAsync()
    {
        courseDto = await GetByIdAsync($"Courses/WithDetails/{Id}");

        if (courseDto is not null)
            breadcrumbItems.AddRange(new List<BreadcrumbItem>
            {
                new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
                new BreadcrumbItem("Courses", href: "/Courses", icon: Icons.Material.Filled.OndemandVideo),
                new BreadcrumbItem(courseDto.Name, href: null, disabled: true),
            });
    }

    private async Task OpenForm(int id)
    {
        DialogOptions dialogOptions = new()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            Position = DialogPosition.Center,
            CloseButton = true
        };

        DialogParameters<CourseForm> formParameters = new()
        {
            { x => x.Id, id }
        };

        var dialog = await DialogService.ShowAsync<CourseForm>("Edit Course", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        CourseGetDetailsDto courseDetails = (CourseGetDetailsDto)result.Data;

        courseDto!.Name = courseDetails.Name;
        courseDto.Description = courseDetails.Description;
        courseDto.IsDeleted = courseDetails.IsDeleted;
        courseDto.IsPaid = courseDetails.IsPaid;
        courseDto.Author = courseDetails.Author;
    }

    private void ChangeDuration((int Duration, bool IsAdded) args)
    {
        if (args.IsAdded)
            courseDto!.DurationInMin += args.Duration;
        else
            courseDto!.DurationInMin -= args.Duration;
    }
}
