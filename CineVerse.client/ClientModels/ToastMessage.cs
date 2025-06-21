using CineVerse.shared.Enums;

namespace CineVerse.client.ClientModels;

public record ToastMessage(Guid Id, string Title, string Body, ToastType Type, int Duration);