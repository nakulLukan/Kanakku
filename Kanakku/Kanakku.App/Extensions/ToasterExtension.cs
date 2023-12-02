
using MudBlazor;

namespace Kanakku.App.Extensions
{
    public static class ToasterExtension
    {
        public static void Success(this ISnackbar toaster,
            string header = "Success",
            string content = "Success",
            int duration = 3)
        {
            toaster.Add(content, Severity.Success);
        }

        public static void Error(this ISnackbar toaster,
            string header,
            string content,
            int duration = 3)
        {
            toaster.Add(content, Severity.Error);

        }
    }
}
