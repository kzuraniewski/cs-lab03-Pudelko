using System.Globalization;

namespace PudelkoLib;

public sealed class Pudelko : IFormattable
{
    private const float MIN_SIZE_METERS = 0f;
    private const float MAX_SIZE_METERS = 10f;
    private const float DEFAULT_SIZE_METERS = 0.1f;

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

    public Pudelko(
        float A = DEFAULT_SIZE_METERS,
        float B = DEFAULT_SIZE_METERS,
        float C = DEFAULT_SIZE_METERS,
        UnitOfMeasure unitOfMeasure = UnitOfMeasure.Meter
    )
    {
        this.A = new Dimension(A, unitOfMeasure);
        this.B = new Dimension(B, unitOfMeasure);
        this.C = new Dimension(C, unitOfMeasure);
    }

    private static void ValidateDimension(Dimension value)
    {
        // FIXME
        if (value.Meters <= MIN_SIZE_METERS) throw new ArgumentOutOfRangeException();
        if (value.Meters >= MAX_SIZE_METERS) throw new ArgumentOutOfRangeException();
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