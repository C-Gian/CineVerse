using CineVerse.client.ClientModels;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class ToastContainerComponent : IDisposable
{
    [Inject] private IToastService ToastService { get; set; } = default!;
    private readonly List<ToastMessage> _toasts = new();

    protected override void OnInitialized()
    {
        ToastService.ToastAdded += AddToast;
    }

    private void AddToast(ToastMessage msg)
    {
        _toasts.Add(msg);
        StateHasChanged();
        _ = DismissLater(msg);
    }

    private async Task DismissLater(ToastMessage msg)
    {
        await Task.Delay(msg.Duration);
        await InvokeAsync(() => RemoveToast(msg));
    }

    private void RemoveToast(ToastMessage msg)
    {
        _toasts.Remove(msg);
        StateHasChanged();
    }

    public void Dispose()
    {
        ToastService.ToastAdded -= AddToast;
    }
}