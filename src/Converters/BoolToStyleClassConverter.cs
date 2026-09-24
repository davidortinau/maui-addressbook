using System.Globalization;

namespace AddressBookPlus.Converters;

public class BoolToStyleClassConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // parameter format: "TrueStyle|FalseStyle" e.g. "btn-primary,btn-sm|btn-outline-primary,btn-sm"
        if (value is bool isSelected && parameter is string styles)
        {
            var parts = styles.Split('|');
            if (parts.Length == 2)
            {
                return isSelected ? parts[0].Split(',').ToList() : parts[1].Split(',').ToList();
            }
        }
        return new List<string> { "btn-outline-primary", "btn-sm" };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
