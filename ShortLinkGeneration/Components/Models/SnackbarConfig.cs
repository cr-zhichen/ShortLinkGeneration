using MudBlazor;

namespace ShortLinkGeneration.Components.Models;

public static class SnackbarConfig
{
    public static Action<SnackbarOptions> DefaultOptions => options =>
    {
        options.SnackbarVariant = Variant.Filled;
        options.ShowTransitionDuration = 300;
        options.HideTransitionDuration = 300;
        options.VisibleStateDuration = 1000;
    };
}
