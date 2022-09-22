using MudBlazor;

namespace Kanakku.UI
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
    }
}
