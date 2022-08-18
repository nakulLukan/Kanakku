namespace Kanakku.Shared;

public static class AppExtension
{
    public static T Clone<T>(this T obj)
    {
        if (obj == null)
        {
            return default(T);
        }
        return System.Text.Json.JsonSerializer.Deserialize<T>(System.Text.Json.JsonSerializer.Serialize(obj));
    }
}
