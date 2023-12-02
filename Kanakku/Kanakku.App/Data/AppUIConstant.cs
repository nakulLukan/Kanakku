using MudBlazor;

namespace Kanakku.App
{
    public static class AppUIConstant
    {
        public static DialogOptions FullScreenDialogOption { get; } = new DialogOptions()
        {
            CloseButton = false,
            FullScreen = true,
            NoHeader = false,
            CloseOnEscapeKey = false,
        };

        public static DialogOptions NormalDialogOption { get; } = new DialogOptions()
        {
            CloseButton = false,
            FullScreen = false,
            NoHeader = false,
            CloseOnEscapeKey = true,
        };

        public static DialogOptions TitleDialogOption { get; } = new DialogOptions()
        {
            DisableBackdropClick = true,
            NoHeader = false,
            CloseOnEscapeKey = true,
        };
    }
}
