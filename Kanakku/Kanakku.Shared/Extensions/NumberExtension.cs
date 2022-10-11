namespace Kanakku.Shared.Extensions;

public static class NumberExtension
{
    public static string ToCurrency(this float value)
    {
        return value.ToString("C", System.Globalization.CultureInfo.CurrentCulture);
    }

    public static string ToCurrencyAsAscii(this float value)
    {
        return value.ToString("0.00");
    }
}
