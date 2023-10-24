namespace Fakrny.Blazor.Pages.Topics;

public partial class TopicForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }

    private TopicDto? topicForm;

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            topicForm = new();
        else
            topicForm = await GetByIdAsync($"Topics/{Id}");

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
        TopicDto? topicDtoResult;

        if (Id == 0)
            (result, topicDtoResult) = await AddAsync("Topics/Add", topicForm!);
        else
            (result, topicDtoResult) = await UpdateAsync($"Topics/Update/{Id}", topicForm!);

        if (result)
            MudDialog.Close(DialogResult.Ok(topicDtoResult));

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
