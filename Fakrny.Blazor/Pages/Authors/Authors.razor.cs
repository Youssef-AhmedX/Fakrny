using System.Net.Http.Json;
using Fakrny.Blazor.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fakrny.Blazor.Pages.Authors;

public partial class Authors
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
        new BreadcrumbItem("Authors", href: null, disabled: true, icon: Icons.Material.Filled.Person),
    };

    private List<AuthorDto>? authors;

    protected override async Task OnInitializedAsync()
    {
        authors = await Http.GetFromJsonAsync<List<AuthorDto>>("https://localhost:44335/api/Authors");

        if (authors is null)
        {

        }
    }

    private async Task ChangeStatus(int id)
    {
        await Http.PutAsync($"https://localhost:44335/api/Authors/ToggleStatus/{id}", null);

        var author = authors!.FirstOrDefault(a => a.Id == id);
        author!.IsDeleted = !author.IsDeleted;
    }

    private void OpenDialog()
    {
        DialogOptions dialog = new() { CloseOnEscapeKey = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, Position = DialogPosition.TopCenter };

        DialogService.Show<AuthorForm>("Add Author", dialog);
    }
}
