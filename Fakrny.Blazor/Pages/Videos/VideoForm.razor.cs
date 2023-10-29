namespace Fakrny.Blazor.Pages.Videos;

public partial class VideoForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter][EditorRequired] public int Id { get; set; }
    [Parameter][EditorRequired] public int? SectionId { get; set; } = null!;

    private VideoPostDto? videoForm;
    private IEnumerable<LookupDto> topics = new List<LookupDto>();
    private IEnumerable<LookupDto> packages = new List<LookupDto>();
    private IEnumerable<LookupDto> libraries = new List<LookupDto>();
    private IEnumerable<LookupDto> languages = new List<LookupDto>();
    private IEnumerable<ReferenceLinkDto> referenceLinks = new List<ReferenceLinkDto>();

    protected override async Task OnParametersSetAsync()
    {
        StartProcessing();

        if (Id == 0)
            videoForm = new();
        else
            videoForm = await GetByIdAsync($"Videos/{Id}");

        topics = await GetAllLookupsAsync<LookupDto>($"Topics");
        packages = await GetAllLookupsAsync<LookupDto>($"Packages");
        libraries = await GetAllLookupsAsync<LookupDto>($"Libraries");
        languages = await GetAllLookupsAsync<LookupDto>($"Languages");
        referenceLinks = await GetAllLookupsAsync<ReferenceLinkDto>($"ReferenceLinks");

        StopProcessing();
    }

    private async Task OnValidSubmit(EditContext context)
    {
        StartProcessing();

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

    private string GetMultiSelectionText(List<string> selectedValues)
    {
        return $"{selectedValues.Count} item{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
    }

    void Cancel() => MudDialog.Cancel();
}
