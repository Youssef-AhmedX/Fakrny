namespace Fakrny.Blazor.Pages.Courses;

public partial class CourseForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }

    private CoursePostDto? courseForm;
    private IEnumerable<LookupDto> authors = new List<LookupDto>();

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            courseForm = new();
        else
            courseForm = await GetByIdAsync($"Courses/{Id}");

        authors = await GetAllLookupsAsync<LookupDto>("Authors");

        StopProcessing();
    }

    private async Task OnValidSubmit(EditContext context)
    {
        StartProcessing();

        if (!context.IsModified())
        {
            Cancel();
            return;
        }

        bool result;
        CoursePostDto? courseDtoResult;

        if (Id == 0)
            (result, courseDtoResult) = await AddAsync("Courses/Add", courseForm!);
        else
            (result, courseDtoResult) = await UpdateAsync($"Courses/Update/{Id}", courseForm!);

        if (result)
        {
            LookupDto author = authors.FirstOrDefault(x => x.Id == courseDtoResult!.AuthorId)!;

            CourseGetDetailsDto courseDto = new()
            {
                Id = courseDtoResult!.Id,
                Name = courseDtoResult!.Name,
                Description = courseDtoResult!.Description,
                IsDeleted = courseDtoResult!.IsDeleted,
                Author = author,
            };

            MudDialog.Close(DialogResult.Ok(courseDto));
        }

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
