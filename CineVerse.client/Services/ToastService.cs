using CineVerse.client.ClientModels;
using CineVerse.client.Services.Interfaces;
using CineVerse.shared.Enums;

namespace CineVerse.client.Services;

public class ToastService : IToastService
{
    public event Action<ToastMessage>? ToastAdded;

    public void Show(string title, string body, ToastType type = ToastType.Info, int duration = 4000)
    {
        ToastAdded?.Invoke(new ToastMessage(Guid.NewGuid(), title, body, type, duration));
    }
}