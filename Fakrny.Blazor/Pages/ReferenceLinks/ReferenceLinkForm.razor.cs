namespace Fakrny.Blazor.Pages.ReferenceLinks;

public partial class ReferenceLinkForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }

    private ReferenceLinkDto? referenceLinkForm;

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            referenceLinkForm = new();
        else
            referenceLinkForm = await GetByIdAsync($"ReferenceLinks/{Id}");

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
        ReferenceLinkDto? referenceLinkDtoResult;

        if (Id == 0)
            (result, referenceLinkDtoResult) = await AddAsync("ReferenceLinks/Add", referenceLinkForm!);
        else
            (result, referenceLinkDtoResult) = await UpdateAsync($"ReferenceLinks/Update/{Id}", referenceLinkForm!);

        if (result)
            MudDialog.Close(DialogResult.Ok(referenceLinkDtoResult));

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
