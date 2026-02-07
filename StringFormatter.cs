using System;
using System.Diagnostics.CodeAnalysis;

namespace StringPuzzle;

internal class StringFormatter
{
    // Characters we will need to clean from the string
    private readonly char leftParen = '(';
    private readonly char rightParen = ')';
    private readonly char comma = ','; 

    // Characters we need to add to the formatting
    private readonly char dash = '-';
    private readonly char spaceCharacter = ' ';
    internal void FormatOutput(string userInput)
    {

        if (userInput == "1")
        {
            FormatBasedOnSelection(userInput);
        }
        else if (userInput == "2")
        {
            FormatBasedOnSelection(userInput);
        }
        else
        {
            Console.WriteLine("Exiting program");
        }
    }
    private void FormatBasedOnSelection(string userInput)
    {
        IFormatter formatter = userInput == "1" ? new DefaultFormatter() : userInput == "2" ? new AlphabeticFormatter() : throw new InvalidDataException();
        var context = new Context();
        context.SetStrategy(formatter);
        context.ExecuteFormatting(InputStringBase.baseString);
    }
}
