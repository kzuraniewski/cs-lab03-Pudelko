using System.Globalization;

namespace PudelkoLib;

public sealed class Pudelko : IFormattable
{
    private const double MIN_SIZE_METERS = 0;
    private const double MAX_SIZE_METERS = 10;
    private const double DEFAULT_SIZE_METERS = 0.1;

    private Dimension _a, _b, _c;
    public Dimension A
    {
        get => _a;
        private set => ValidateDimension(_a = value);
    }
    public Dimension B
    {
        get => _b;
        private set => ValidateDimension(_b = value);
    }
    public Dimension C
    {
        get => _c;
        private set => ValidateDimension(_c = value);
    }

    public double Objetosc => Math.Round(A.Meters * B.Meters * C.Meters, 9);

    public double Pole => Math.Round(2 * (A.Meters * B.Meters + B.Meters * C.Meters + A.Meters * C.Meters), 6);

    public Pudelko(
        double A = DEFAULT_SIZE_METERS,
        double B = DEFAULT_SIZE_METERS,
        double C = DEFAULT_SIZE_METERS,
        UnitOfMeasure unitOfMeasure = UnitOfMeasure.Meter
    )
    {
        this.A = new Dimension(A, unitOfMeasure);
        this.B = new Dimension(B, unitOfMeasure);
        this.C = new Dimension(C, unitOfMeasure);
    }

    private static void ValidateDimension(Dimension value)
    {
        if (value.Meters <= MIN_SIZE_METERS) throw new ArgumentOutOfRangeException(nameof(value));
        if (value.Meters >= MAX_SIZE_METERS) throw new ArgumentOutOfRangeException(nameof(value));
    }

    public string ToString(string? format = "m", IFormatProvider? formatProvider = null)
    {
        if (String.IsNullOrEmpty(format)) format = "m";
        formatProvider ??= CultureInfo.CurrentCulture;

        string[] FormatDimensions(params Dimension[] dimensions) => 
            dimensions.Select(dimension => dimension.ToString(format, formatProvider)).ToArray();

        return String.Join(" × ", FormatDimensions(A, B, C));
    }
}