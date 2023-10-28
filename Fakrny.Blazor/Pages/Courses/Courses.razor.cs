namespace Fakrny.Blazor.Pages.Courses;

public partial class Courses
{
    private List<CourseDto> courses = new();
    private string searchString = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        StartProcessing();

        breadcrumbItems.AddRange(new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Courses", href: null, disabled: true, icon: Icons.Material.Filled.OndemandVideo),
        });

        courses = await GetAllAsync("Courses");

        StopProcessing();
    }

    private async Task ChangeStatus(int id, string objectName)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"Courses/ToggleStatus/{id}");

            if (!result)
                return;

            var course = courses!.FirstOrDefault(a => a.Id == id);
            course!.IsDeleted = !course.IsDeleted;
        }

        StopProcessing();
    }

    private async Task OpenForm(int id = 0)
    {
        DialogOptions dialogOptions = new()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
            Position = DialogPosition.Center,
            CloseButton = true
        };

        DialogParameters<CourseForm> formParameters = new()
        {
            { x => x.Id, id }
        };

        var dialog = await DialogService.ShowAsync<CourseForm>(id == 0 ? "Add Course" : "Edit Course", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var author = courses.FirstOrDefault(a => a.Id == id);
            if (author is not null)
                courses.Remove(author);
        }

        CourseGetDetailsDto courseDetails = (CourseGetDetailsDto)result.Data;

        CourseDto courseDto = new()
        {
            Id = courseDetails.Id,
            Name = courseDetails.Name,
            Author = courseDetails.Author,
            DurationInMin = courseDetails.DurationInMin,
            IsDeleted = courseDetails.IsDeleted,
            IsPaid = courseDetails.IsPaid,
        };

        courses.Add(courseDto);
    }

    //TODO : Work on filter for course
    private bool FilterFunc(CourseDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(CourseDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Author.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
