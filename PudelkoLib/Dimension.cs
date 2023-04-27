using System.Globalization;

namespace PudelkoLib;

public struct Dimension : IFormattable
{
    public double Meters { get; set; }

    public double Centimeters
    {
        get => Meters * 100;
        set => Meters = value / 100;
    }

    public double Milimeters
    {
        get => Meters * 1000;
        set => Meters = value / 1000;
    }

    public Dimension(double value, UnitOfMeasure unitOfMeasure = UnitOfMeasure.Meter)
    {
        Meters = unitOfMeasure switch
        {
            UnitOfMeasure.Milimeter => value / 1000,
            UnitOfMeasure.Centimeter => value / 100,
            UnitOfMeasure.Meter => value,
            _ => throw new NotImplementedException(),
        };
    }

    public static implicit operator Dimension(double v) => new Dimension(v);

    public string ToString(string? format = "m", IFormatProvider? formatProvider = null)
    {
        if (String.IsNullOrEmpty(format)) format = "m";
        formatProvider ??= CultureInfo.CurrentCulture;

        return format.Trim().ToLowerInvariant() switch
        {
            "m" => $"{Meters.ToString("0.000", formatProvider)} m",
            "cm" => $"{Centimeters.ToString("0.0", formatProvider)} cm",
            "mm" => $"{Milimeters.ToString("F0", formatProvider)} mm",
            _ => throw new FormatException("Invalid unit format")
        };
    }
}