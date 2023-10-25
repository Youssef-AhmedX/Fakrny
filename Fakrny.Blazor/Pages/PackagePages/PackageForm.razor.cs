namespace Fakrny.Blazor.Pages.Packages;

public partial class PackageForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }

    private PackageDto? packageForm;

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            packageForm = new();
        else
            packageForm = await GetByIdAsync($"Packages/{Id}");

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
        PackageDto? packageDtoResult;

        if (Id == 0)
            (result, packageDtoResult) = await AddAsync("Packages/Add", packageForm!);
        else
            (result, packageDtoResult) = await UpdateAsync($"Packages/Update/{Id}", packageForm!);

        if (result)
            MudDialog.Close(DialogResult.Ok(packageDtoResult));

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
