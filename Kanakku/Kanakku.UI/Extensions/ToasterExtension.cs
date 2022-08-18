using BlazorStrap;

namespace Kanakku.UI.Extensions
{
    public static class ToasterExtension
    {
        public static void Success(this Toaster toaster, string header, string content, int duration = 3)
        {
            toaster.Add(header, content, o =>
            {
                o.Color = BSColor.Success;
                o.CloseAfter = duration * 1000;
            });
        }

        public static void Error(this Toaster toaster, string header, string content, int duration = 3)
        {
            toaster.Add(header, content, o =>
            {
                o.Color = BSColor.Danger;
                o.CloseAfter = duration * 1000;
            });
        }
    }
}
