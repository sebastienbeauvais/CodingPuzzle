using System;
using System.Xml.Serialization;

namespace StringPuzzle;

public class DefaultFormatter : IFormatter
{
    public void FormatString(string inputString)
    {
        int indentationLevel = -1;
        foreach(var word in inputString.Split())
        {
            indentationLevel = HandleWord(word, indentationLevel);
        }
    }
    private int HandleWord(string word, int indentationLevel)
    {
        // Using a list<char> to store the final word as it is more efficient to convert this to a string then appending one character at a time
        var result = new List<char>();
        for(int i = 0 ; i < word.Length; i++)
        {
            if(word[i] == StringFormatter.leftParen)
            {
                HandleLeftParen(i, word, result, indentationLevel);
                indentationLevel++;
            }
            else if (word[i] == StringFormatter.rightParen)
            {
                HandleRightParen(i, word, result, indentationLevel);
                indentationLevel--;
            }
            else if(word[i] == StringFormatter.comma)
            {
                continue;
            }
            else
            {
                result.Add(word[i]);
            }
        }
        if(result.Count > 0)
        {
            BuildWord(result, indentationLevel);
        }
        return indentationLevel;
    }
    private void BuildWord(List<char> result, int indentationLevel)
    {
        if(result.Count > 0)
        {
            HandleIndentation(indentationLevel);
            var word = string.Join("", result.ToArray());
            Console.WriteLine($"{StringFormatter.dash} {word}");
        }
    }
    private void HandleIndentation(int indentationLevel)
    {
        for(int i = 0; i < indentationLevel ; i++)
        {
            Console.Write(StringFormatter.spaceCharacter);
        }
    }
    private void HandleLeftParen(int i, string word, List<char> result, int indentationLevel)
    {
        // If we reach a character in the given word that would increase the indentation level
        // EX: customFields(c1 is a word that is returned from String.Split() with the example input
            // If we dont handle this case we would print out customFields(c1 all on the 
            // same line AND not increase indentation for the nested obejct 
        // We also clear the current result as we have a nested word we then can handle
        if(i < word.Length && i > 0)
        {
            BuildWord(result, indentationLevel);
            result.Clear();
        }
    }
    private void HandleRightParen(int i, string word, List<char> result, int indentationLevel)
    {
        if(i < word.Length)
        {
            BuildWord(result, indentationLevel);
            result.Clear();
        }
    }
}
