namespace Fakrny.Blazor.Pages.Libraries;

public partial class Libraries
{
    private List<LibraryDto> libraries = new();
    private string searchString = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        StartProcessing();

        breadcrumbItems.AddRange(new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Libraries", href: null, disabled: true, icon: Icons.Material.Filled.Code),
        });

        libraries = await GetAllAsync("Libraries");

        StopProcessing();
    }

    private async Task ChangeStatus(int id, string objectName)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"Libraries/ToggleStatus/{id}");

            if (!result)
                return;

            var language = libraries!.FirstOrDefault(a => a.Id == id);
            language!.IsDeleted = !language.IsDeleted;
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

        DialogParameters<LibraryForm> formParameters = new()
        {
            { x => x.Id, id }
        };

        var dialog = await DialogService.ShowAsync<LibraryForm>(id == 0 ? "Add Language" : "Edit Language", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var language = libraries.FirstOrDefault(a => a.Id == id);
            if (language is not null)
                libraries.Remove(language);
        }

        libraries.Add((LibraryDto)result.Data);
    }

    private bool FilterFunc(LibraryDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(LibraryDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
