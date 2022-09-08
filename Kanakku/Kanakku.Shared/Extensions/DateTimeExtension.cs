namespace Kanakku.Shared.Extensions;

public static class DateTimeExtension
{
    public static DateTime? ToDateTimeKind(this DateTime? value, DateTimeKind datetimeKind = DateTimeKind.Utc)
    {
        if (!value.HasValue)
        {
            return null;
        }
        return new DateTime(value.Value.Ticks, datetimeKind);
    }

    public static DateTime ToDateTimeKind(this DateTime value, DateTimeKind datetimeKind = DateTimeKind.Utc)
    {
        return new DateTime(value.Ticks, datetimeKind);
    }
}
