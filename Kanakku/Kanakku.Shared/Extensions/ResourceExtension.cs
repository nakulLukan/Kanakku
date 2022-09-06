namespace Kanakku.Shared.Extensions
{
    public static class ResourceExtension
    {
        public static string ToImgSource(this string base64)
        {
            return string.Format("data:image/png;base64,{0}", base64);
        }


        public static string ToProductImgSource(this string base64)
        {
            return !string.IsNullOrEmpty(base64) ?
                string.Format("data:image/png;base64,{0}", base64)
                : DirectoryConstant.DFAULT_PRODUCT_IMAGE_PLACEHOLDER;
        }

        public static string ToEmployeeImgSource(this string base64)
        {
            return !string.IsNullOrEmpty(base64) ?
                string.Format("data:image/png;base64,{0}", base64)
                : DirectoryConstant.DFAULT_PRODUCT_IMAGE_PLACEHOLDER;
        }

        public static string ToDefaultImgSource(this string base64)
        {
            return !string.IsNullOrEmpty(base64) ?
                string.Format("data:image/png;base64,{0}", base64)
                : DirectoryConstant.DFAULT_IMAGE_PLACEHOLDER;
        }
    }
}
