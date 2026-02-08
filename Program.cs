using StringPuzzle;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("This program aims to format a string to represent hierarchical structure.");
        Console.WriteLine();
        Console.WriteLine("It will take the following string as input:");
        Console.WriteLine(InputStringBase.baseString);
        Console.WriteLine("");
        Console.WriteLine("Would you like to sort in order or alphabetically?");
        Console.WriteLine("1 = in order | 2 = alphabeticall | Any other key to exit");
        string? userInput = Console.ReadLine();
        StringFormatter stringFormatter = new StringFormatter();
        stringFormatter.FormatOutput(userInput!);
    }
}
