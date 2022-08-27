using Kanakku.Shared.Models;

namespace Kanakku.Shared.Extensions;

public static class FormExtension
{
    public static string IsInvalid(this FormError err, string propertyName)
    {
        return err != null && err.ContainsKey(propertyName) && err[propertyName].Any() 
            ? "is-invalid" : string.Empty;
    }

    public static string GetError(this FormError err, string propertyName)
    {
        return err != null && err.ContainsKey(propertyName)
            ? err[propertyName].First() : string.Empty;
    }
}
