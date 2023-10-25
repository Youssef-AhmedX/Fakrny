namespace Fakrny.Blazor.Pages.Packages;

public partial class Packages
{
    private List<PackageDto> packages = new();
    private string searchString = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        StartProcessing();

        breadcrumbItems.AddRange(new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Packages", href: null, disabled: true, icon: Icons.Material.Filled.Code),
        });

        packages = await GetAllAsync("Packages");

        StopProcessing();
    }

    private async Task ChangeStatus(int id, string objectName)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"Packages/ToggleStatus/{id}");

            if (!result)
                return;

            var package = packages!.FirstOrDefault(a => a.Id == id);
            package!.IsDeleted = !package.IsDeleted;
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

        DialogParameters<PackageForm> formParameters = new()
        {
            { x => x.Id, id }
        };

        var dialog = await DialogService.ShowAsync<PackageForm>(id == 0 ? "Add Package" : "Edit Package", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var package = packages.FirstOrDefault(a => a.Id == id);
            if (package is not null)
                packages.Remove(package);
        }

        packages.Add((PackageDto)result.Data);
    }

    private bool FilterFunc(PackageDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(PackageDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
