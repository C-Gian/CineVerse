using CineVerse.client.ClientModels;
using CineVerse.shared.Enums;

namespace CineVerse.client.Services.Interfaces;

public interface IToastService
{
    event Action<ToastMessage>? ToastAdded;
    void Show(string title, string body, ToastType type = ToastType.Info, int duration = 4000);
}