namespace Fakrny.Blazor.Pages.Topics;

public partial class Topics
{
    private List<TopicDto> topics = new();
    private string searchString = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        StartProcessing();

        breadcrumbItems.AddRange(new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Topics", href: null, disabled: true, icon: Icons.Material.Filled.Code),
        });

        topics = await GetAllAsync("Topics");

        StopProcessing();
    }

    private async Task ChangeStatus(int id, string objectName)
    {
        StartProcessing();

        var isConfirmed = await ShowConfirmation($"You will Change Status for ({objectName}).");

        if (isConfirmed)
        {
            var result = await ChangeStatusAsync($"Topics/ToggleStatus/{id}");

            if (!result)
                return;

            var topic = topics!.FirstOrDefault(a => a.Id == id);
            topic!.IsDeleted = !topic.IsDeleted;
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

        DialogParameters<TopicForm> formParameters = new()
        {
            { x => x.Id, id }
        };

        var dialog = await DialogService.ShowAsync<TopicForm>(id == 0 ? "Add Topic" : "Edit Topic", formParameters, dialogOptions);
        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (id != 0)
        {
            var topic = topics.FirstOrDefault(a => a.Id == id);
            if (topic is not null)
                topics.Remove(topic);
        }

        topics.Add((TopicDto)result.Data);
    }

    private bool FilterFunc(TopicDto element) => ImpFilterFunc(element, searchString);

    private bool ImpFilterFunc(TopicDto element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}
