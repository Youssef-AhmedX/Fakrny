﻿namespace Fakrny.Blazor.Pages.Authors;

public partial class Authors
{
    private List<AuthorDto> authors = new();
    private string searchString = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        StartProcessing();

        breadcrumbItems.AddRange(new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Authors", href: null, disabled: true, icon: Icons.Material.Filled.Person),
        });

        authors = await GetAllAsync("Authors");

        StopProcessing();
    }

    private async Task ChangeStatus(int id, string objectName)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"Authors/ToggleStatus/{id}");

            if (!result)
                return;

            var author = authors!.FirstOrDefault(a => a.Id == id);
            author!.IsDeleted = !author.IsDeleted;
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

        DialogParameters<AuthorForm> formParameters = new()
        {
            { x => x.Id, id }
        };

        var dialog = await DialogService.ShowAsync<AuthorForm>(id == 0 ? "Add Author" : "Edit Author", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var author = authors.FirstOrDefault(a => a.Id == id);
            if (author is not null)
                authors.Remove(author);
        }

        authors.Add((AuthorDto)result.Data);
    }

    private bool FilterFunc(AuthorDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(AuthorDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
