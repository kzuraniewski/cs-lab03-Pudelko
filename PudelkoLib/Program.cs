namespace PudelkoLib;

internal class Program
{
    private static void Main(string[] args)
    {
        Pudelko p = new(1, 2, 3);
        Pudelko pp = new(1, 1, 2);
        Console.WriteLine(Pudelko.Parse("1m × 200cm ×1mm"));

        //foreach (var x in p) Console.WriteLine(x);

        Console.ReadLine();
    }
}