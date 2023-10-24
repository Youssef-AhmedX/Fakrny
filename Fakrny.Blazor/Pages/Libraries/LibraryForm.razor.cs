namespace Fakrny.Blazor.Pages.Libraries;

public partial class LibraryForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }

    private LibraryDto? libraryForm;

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            libraryForm = new();
        else
            libraryForm = await GetByIdAsync($"Libraries/{Id}");

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
        LibraryDto? libraryDtoResult;

        if (Id == 0)
            (result, libraryDtoResult) = await AddAsync("Libraries/Add", libraryForm!);
        else
            (result, libraryDtoResult) = await UpdateAsync($"Libraries/Update/{Id}", libraryForm!);

        if (result)
            MudDialog.Close(DialogResult.Ok(libraryDtoResult));

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
