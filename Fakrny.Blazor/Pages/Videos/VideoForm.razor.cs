namespace Fakrny.Blazor.Pages.Videos;

public partial class VideoForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }
    [Parameter][EditorRequired] public int? SectionId { get; set; } = null!;

    private VideoPostDto? videoForm;

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            videoForm = new();
        else
            videoForm = await GetByIdAsync($"Videos/{Id}");

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
        VideoPostDto? videoDtoResult;

        videoForm!.SectionId = (int)SectionId!;

        if (Id == 0)
            (result, videoDtoResult) = await AddAsync("Videos/Add", videoForm!);
        else
            (result, videoDtoResult) = await UpdateAsync($"Videos/Update/{Id}", videoForm!);

        if (result)
        {

            VideoDetailsDto videoDto = new()
            {
                Id = videoDtoResult!.Id,
                Name = videoDtoResult!.Name,
                Number = videoDtoResult!.Number,
                OrderIndex = videoDtoResult!.OrderIndex,
                DurationInMin = videoDtoResult!.DurationInMin,
                Description = videoDtoResult!.Description,
                IsDeleted = videoDtoResult!.IsDeleted,
            };

            MudDialog.Close(DialogResult.Ok(videoDto));
        }

        StopProcessing();
    }

    void Cancel() => MudDialog.Cancel();
}
