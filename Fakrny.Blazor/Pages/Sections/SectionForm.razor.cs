namespace Fakrny.Blazor.Pages.Sections;

public partial class SectionForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }
    [Parameter][EditorRequired] public int? CourseId { get; set; } = null!;

    private SectionPostDto? sectionForm;

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            sectionForm = new();
        else
            sectionForm = await GetByIdAsync($"Sections/{Id}");

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
        SectionPostDto? sectionDtoResult;

        sectionForm!.CourseId = (int)CourseId!;

        if (Id == 0)
            (result, sectionDtoResult) = await AddAsync("Sections/Add", sectionForm!);
        else
            (result, sectionDtoResult) = await UpdateAsync($"Sections/Update/{Id}", sectionForm!);

        if (result)
        {

            SectionDetailsDto sectionDto = new()
            {
                Id = sectionDtoResult!.Id,
                Name = sectionDtoResult!.Name,
                Number = sectionDtoResult!.Number,
                OrderIndex = sectionDtoResult!.OrderIndex,
                Description = sectionDtoResult!.Description,
                IsDeleted = sectionDtoResult!.IsDeleted,
            };

            MudDialog.Close(DialogResult.Ok(sectionDto));
        }

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
