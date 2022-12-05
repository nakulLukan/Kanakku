namespace Kanakku.Shared.Extensions;

public static class NumberExtension
{
    public static string ToCurrency(this float value)
    {
        return value.ToString("C", System.Globalization.CultureInfo.CurrentCulture);
    }

    public static string ToCurrency(this float? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToString("C", System.Globalization.CultureInfo.CurrentCulture);
        }

        return String.Empty;
    }

    public static string ToCurrencyAsAscii(this float value)
    {
        return value.ToString("0.00");
    }

    public static string ToCurrencyAsAscii(this float? value)
    {
        if (value.HasValue)
        {
            return ToCurrencyAsAscii(value.Value);
        }

        return String.Empty;
    }
}
