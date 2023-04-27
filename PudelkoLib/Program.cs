namespace PudelkoLib;

internal class Program
{
    private static void Main(string[] args)
    {
        Pudelko p = new(2.5f, 9.321f, 0.1f);  
        Console.WriteLine(p.ToString());
        Console.ReadLine();
    }
}