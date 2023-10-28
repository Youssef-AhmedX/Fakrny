namespace Fakrny.Blazor.Pages.Sections;

public partial class Sections
{
    [Parameter][EditorRequired] public List<SectionDto> SectionsList { get; set; } = null!;
    [Parameter][EditorRequired] public int CourseId { get; set; }
    [Parameter][EditorRequired] public EventCallback<(int Duration, bool IsAdded)> OnDurationChanged { get; set; }


    private string searchString = string.Empty;

    private async Task OpenForm(int id = 0)
    {
        DialogOptions dialogOptions = new()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            Position = DialogPosition.Center,
            CloseButton = true
        };

        DialogParameters<SectionForm> formParameters = new()
        {
            { x => x.Id, id },
            { x => x.CourseId, CourseId },
        };

        var dialog = await DialogService.ShowAsync<SectionForm>(id == 0 ? "Add Section" : "Edit Section", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var section = SectionsList.FirstOrDefault(a => a.Id == id);
            if (section is not null)
            {
                SectionsList.Remove(section);
                await OnDurationChanged.InvokeAsync((Duration: section.DurationInMin, IsAdded: false));
            }
        }

        SectionDetailsDto sectionDetails = (SectionDetailsDto)result.Data;

        SectionDto sectionDto = new()
        {
            Id = sectionDetails.Id,
            Name = sectionDetails.Name,
            Number = sectionDetails.Number,
            OrderIndex = sectionDetails.OrderIndex,
            IsDeleted = sectionDetails.IsDeleted,
        };

        await OnDurationChanged.InvokeAsync((Duration: sectionDto.DurationInMin, IsAdded: true));

        SectionsList.Add(sectionDto);
    }

    private async Task ChangeStatus(int? id, string objectName)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"Sections/ToggleStatus/{id}");

            if (!result)
                return;

            var section = SectionsList.FirstOrDefault(a => a.Id == id);
            section!.IsDeleted = !section.IsDeleted;

            await OnDurationChanged.InvokeAsync((Duration: section.DurationInMin, IsAdded: !section.IsDeleted));
        }

        StopProcessing();
    }

    private bool FilterFunc(SectionDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(SectionDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
