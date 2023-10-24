namespace Fakrny.Blazor.Pages.ReferenceLinks;

public partial class ReferenceLinks
{
    private List<ReferenceLinkDto> referenceLinks = new();
    private string searchString = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        StartProcessing();

        breadcrumbItems.AddRange(new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("ReferenceLinks", href: null, disabled: true, icon: Icons.Material.Filled.Code),
        });

        referenceLinks = await GetAllAsync("ReferenceLinks");

        StopProcessing();
    }

    private async Task ChangeStatus(int id, string objectName)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"ReferenceLinks/ToggleStatus/{id}");

            if (!result)
                return;

            var referenceLink = referenceLinks!.FirstOrDefault(a => a.Id == id);
            referenceLink!.IsDeleted = !referenceLink.IsDeleted;
        }

        StopProcessing();
    }

    private async Task OpenForm(int id = 0)
    {
        DialogOptions dialogOptions = new()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.ExtraSmall,
            FullWidth = true,
            Position = DialogPosition.Center,
            CloseButton = true
        };

        DialogParameters<ReferenceLinkForm> formParameters = new()
        {
            { x => x.Id, id }
        };

        var dialog = await DialogService.ShowAsync<ReferenceLinkForm>(id == 0 ? "Add ReferenceLink" : "Edit ReferenceLink", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var referenceLink = referenceLinks.FirstOrDefault(a => a.Id == id);
            if (referenceLink is not null)
                referenceLinks.Remove(referenceLink);
        }

        referenceLinks.Add((ReferenceLinkDto)result.Data);
    }

    private bool FilterFunc(ReferenceLinkDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(ReferenceLinkDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Link.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
