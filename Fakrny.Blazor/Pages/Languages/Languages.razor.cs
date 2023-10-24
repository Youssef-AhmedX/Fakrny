namespace Fakrny.Blazor.Pages.Languages;

public partial class Languages
{
    private List<LanguageDto> languages = new();
    private string searchString = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        StartProcessing();

        breadcrumbItems.AddRange(new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Languages", href: null, disabled: true, icon: Icons.Material.Filled.Code),
        });

        languages = await GetAllAsync("Languages");

        StopProcessing();
    }

    private async Task ChangeStatus(int id, string objectName)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"Languages/ToggleStatus/{id}");

            if (!result)
                return;

            var language = languages!.FirstOrDefault(a => a.Id == id);
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

        DialogParameters<LanguageForm> formParameters = new()
        {
            { x => x.Id, id }
        };

        var dialog = await DialogService.ShowAsync<LanguageForm>(id == 0 ? "Add Language" : "Edit Language", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var language = languages.FirstOrDefault(a => a.Id == id);
            if (language is not null)
                languages.Remove(language);
        }

        languages.Add((LanguageDto)result.Data);
    }

    private bool FilterFunc(LanguageDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(LanguageDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
