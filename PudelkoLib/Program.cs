namespace PudelkoLib;

internal class Program
{
    private static void Main(string[] args)
    {
        Pudelko p = new(2, 2.5123441251, 2.42142333112);  
        Console.WriteLine(p.Pole);
        Console.ReadLine();
    }
}