namespace Fakrny.Blazor.Pages.Videos;

public partial class Videos
{
    [Parameter][EditorRequired] public List<VideoDto> VideosList { get; set; } = null!;
    [Parameter][EditorRequired] public int SectionId { get; set; }
    [Parameter][EditorRequired] public EventCallback<(int Duration, bool IsAdded)> OnDurationChanged { get; set; }

    private string searchString = string.Empty;

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

        DialogParameters<VideoForm> formParameters = new()
        {
            { x => x.Id, id },
            { x => x.SectionId, SectionId },
        };

        var dialog = await DialogService.ShowAsync<VideoForm>(id == 0 ? "Add Video" : "Edit Video", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var video = VideosList.FirstOrDefault(a => a.Id == id);
            if (video is not null)
            {
                VideosList.Remove(video);
                await OnDurationChanged.InvokeAsync((Duration: video.DurationInMin, IsAdded: false));
            }
        }

        VideoDetailsDto videoDetails = (VideoDetailsDto)result.Data;

        VideoDto videoDto = new()
        {
            Id = videoDetails.Id,
            Name = videoDetails.Name,
            Number = videoDetails.Number,
            OrderIndex = videoDetails.OrderIndex,
            DurationInMin = videoDetails.DurationInMin,
            IsDeleted = videoDetails.IsDeleted,
        };

        await OnDurationChanged.InvokeAsync((Duration: videoDto.DurationInMin, IsAdded: true));

        VideosList.Add(videoDto);
    }

    private async Task ChangeStatus(int? id, string objectName, int videoDuration)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"Videos/ToggleStatus/{id}");

            if (!result)
                return;

            var video = VideosList.FirstOrDefault(a => a.Id == id);
            video!.IsDeleted = !video.IsDeleted;

            await OnDurationChanged.InvokeAsync((Duration: videoDuration, IsAdded: !video.IsDeleted));
        }

        StopProcessing();
    }

    private bool FilterFunc(VideoDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(VideoDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
