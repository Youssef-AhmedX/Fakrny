﻿namespace Fakrny.Blazor.Pages.Authors;

public partial class AuthorForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }

    private AuthorDto? authorForm;

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            authorForm = new();
        else
            authorForm = await GetByIdAsync($"Authors/{Id}");

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
        AuthorDto? authorDtoResult;

        if (Id == 0)
            (result, authorDtoResult) = await AddAsync("Authors/Add", authorForm!);
        else
            (result, authorDtoResult) = await UpdateAsync($"Authors/Update/{Id}", authorForm!);

        if (result)
            MudDialog.Close(DialogResult.Ok(authorDtoResult));

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
