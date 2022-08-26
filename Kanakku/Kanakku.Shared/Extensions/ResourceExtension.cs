namespace Kanakku.Shared.Extensions
{
    public static class ResourceExtension
    {
        public static string ToImgSource(this string base64)
        {
            return string.Format("data:image/png;base64,{0}", base64);
        }
    }
}
