using CineVerse.client.ClientModels;
using CineVerse.shared.Enums;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class ToastComponent
{
    [Parameter] public ToastMessage Message { get; set; } = default!;
    [Parameter] public EventCallback<ToastMessage> OnDismiss { get; set; }

    private string CssClass => Message.Type switch
    {
        ToastType.Success => "success",
        ToastType.Warning => "warning",
        ToastType.Error => "error",
        _ => "info"
    };
}