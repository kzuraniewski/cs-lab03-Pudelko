using System.Globalization;

namespace PudelkoLib;

public sealed class Pudelko : IFormattable, IEquatable<Pudelko>
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

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Pudelko p = (Pudelko)obj;
            return Equals(p);
        }
    }

    public bool Equals(Pudelko? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        static List<double> GetDimensionsSorted(Pudelko box) => 
            new List<double>() { box.A.Meters, box.B.Meters, box.C.Meters }.OrderBy(d => d).ToList();

        return GetDimensionsSorted(this).SequenceEqual(GetDimensionsSorted(other));
    }

    public static bool Equals(Pudelko? p1, Pudelko? p2)
    {
        if (p1 is null) return p2 is null;
        return p1.Equals(p2);
    }

    public override int GetHashCode() => HashCode.Combine(A, B, C);

    public static bool operator ==(Pudelko? p1, Pudelko? p2) => Equals(p1, p2);

    public static bool operator !=(Pudelko? p1, Pudelko? p2) => !(p1 == p2);
}