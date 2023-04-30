namespace PudelkoLib;

internal class Program
{
    private static void Main(string[] args)
    {
        Pudelko p = new(1, 2, 3);
        Pudelko pp = new(1, 3, 2);
        Console.WriteLine(p == pp);
        Console.ReadLine();
    }
}