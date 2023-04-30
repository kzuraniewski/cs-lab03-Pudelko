using System.Globalization;

namespace PudelkoLib;

// todo pkt 8

public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IComparable<Pudelko>
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

    public Dimension MinDimension => new Dimension(new List<double>() { A.Meters, B.Meters, C.Meters }.Min());

    public Dimension MaxDimension => new Dimension(new List<double>() { A.Meters, B.Meters, C.Meters }.Max());

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

    public override int GetHashCode() => HashCode.Combine(A, B, C);

    public bool Equals(Pudelko? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        static double[] GetDimensionsSorted(Pudelko box) =>
            ((double[])box).OrderBy(d => d).ToArray();

        return GetDimensionsSorted(this).SequenceEqual(GetDimensionsSorted(other));
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

    public static bool Equals(Pudelko? p1, Pudelko? p2)
    {
        if (p1 is null) return p2 is null;
        return p1.Equals(p2);
    }

    public int CompareTo(Pudelko? other)
    {
        if (other is null) return 1;

        return Objetosc.CompareTo(other.Objetosc);
    }

    public static bool operator ==(Pudelko? p1, Pudelko? p2) => Equals(p1, p2);
    
    public static bool operator !=(Pudelko? p1, Pudelko? p2) => !(p1 == p2);

    public static bool operator >(Pudelko? p1, Pudelko? p2) => p1 is not null && p1.CompareTo(p2) == 1;
    
    public static bool operator >=(Pudelko? p1, Pudelko? p2) => !(p1 < p2);

    public static bool operator <(Pudelko? p1, Pudelko? p2) => p1 is not null && p1.CompareTo(p2) == -1;
    
    public static bool operator <=(Pudelko? p1, Pudelko? p2) => !(p1 > p2);

    /// <summary>
    /// Casts Pudelko to an array of its dimensions in meters.
    /// </summary>
    public static explicit operator double[](Pudelko p) => new double[3] { p.A.Meters, p.B.Meters, p.C.Meters };

    /// <summary>
    /// Casts a tuple of box dimensions (in milimeters) to Pudelko.
    /// </summary>
    /// <param name="dimensions">Box dimensions in milimeters</param>
    public static implicit operator Pudelko(ValueTuple<int, int, int> dimensions) =>
        new Pudelko(
            dimensions.Item1, 
            dimensions.Item2,
            dimensions.Item3,
            UnitOfMeasure.Milimeter
        );
}