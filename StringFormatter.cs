using System;
using System.Diagnostics.CodeAnalysis;

namespace StringPuzzle;

internal class StringFormatter
{
    // Characters we will need to clean from the string
    internal const char leftParen = '(';
    internal const char rightParen = ')';
    internal const char comma = ','; 

    // Characters we need to add to the formatting
    internal const char dash = '-';
    internal const char spaceCharacter = ' ';
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
        else if(userInput == "3")
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
        // Add option 3 but how?
        IFormatter formatter = userInput == "1" ? new DefaultFormatter() : userInput == "2" ? new AlphabeticFormatter() : throw new InvalidDataException();
        var context = new Context();
        context.SetStrategy(formatter);
        context.ExecuteFormatting(InputStringBase.baseString);
    }
}
