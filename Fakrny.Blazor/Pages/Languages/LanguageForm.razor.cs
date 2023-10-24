namespace Fakrny.Blazor.Pages.Languages;

public partial class LanguageForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }

    private LanguageDto? languageForm;

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            languageForm = new();
        else
            languageForm = await GetByIdAsync($"Languages/{Id}");

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
        LanguageDto? languageDtoResult;

        if (Id == 0)
            (result, languageDtoResult) = await AddAsync("Languages/Add", languageForm!);
        else
            (result, languageDtoResult) = await UpdateAsync($"Languages/Update/{Id}", languageForm!);

        if (result)
            MudDialog.Close(DialogResult.Ok(languageDtoResult));

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
