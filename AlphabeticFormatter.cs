using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace StringPuzzle;

public class AlphabeticFormatter : IFormatter
{
    public void FormatString(string inputString)
    {
        // Current idea is to parse the input string and build a List<Tuple> where the first value in the tuple
        // is the index level and the second value in the tuple is the word to print.
        // We should then be able to sort by index level ASC then word asc and print the output
        var wordsWithIndentationLevel = new List<(int, string)>();
        // Do something similar to Default Formatter but instead of printing out the word we return a tuple (int, string)?
        int indentationLevel = -1;
        foreach (string word in inputString.Split())
        {
            // There is bug within this function. Not handling "(" or ")" correctly atm. potentially can string.Split on "(" or ")"
            var tuple = PrepareWordsForPrinting(word, indentationLevel);
            indentationLevel = tuple.Item1;
            wordsWithIndentationLevel.Add(tuple);
        }
        PrintWordsInAlphabeticalOrder(wordsWithIndentationLevel);
        
    }
    private (int, string) PrepareWordsForPrinting(string word, int indentationLevel)
    {
        List<char> cleanedWord = new List<char>();
        // I needed a way to track if I am at the end of the word while iterating over with a foreach loop so characterIdx was born!
        // Personally I think they are more readible that a for loop where we would have word[i] representing a character
        var characterIdx = -1;
        foreach(char character in word)
        {
            characterIdx++;
            if(character == StringFormatter.leftParen)
            {
                var output = HandleLeftParen(character, characterIdx, word, cleanedWord); // Update to meaningful variable name
                indentationLevel++;
            }
            else if(character == StringFormatter.rightParen)
            {
                var output = HandleRightParen(character, characterIdx, word, cleanedWord);
                indentationLevel--;
            }
            else if(character == StringFormatter.comma)
            {
                continue;
            }
            else
            {
                cleanedWord.Add(character);
            }
        }
        return (indentationLevel, string.Join("", cleanedWord.ToArray()));
    }
    private string? HandleLeftParen(char character, int characterIdx, string word, List<char> cleanedWord)
    {
        // This is the case where we want to build our word earlier than the end of the string
        string earlyWord = string.Empty;
        if(characterIdx < word.Length && characterIdx > 0)
        {
            earlyWord = BuildWord(cleanedWord);
            // Still need to handle when we have more characters after 
        }
        return earlyWord != string.Empty ? earlyWord : null;
        
    }
    private string HandleRightParen(char character, int characterIdx, string word, List<char> cleanedWord)
    {
        var builtWord = string.Empty;
        if(characterIdx < word.Length)
        {
            builtWord = BuildWord(cleanedWord);
        }
        return builtWord;
    }
    private string BuildWord(List<char> cleanedWord)
    {
        return string.Join("", cleanedWord.ToArray());
    }
    private void PrintWordsInAlphabeticalOrder(List<(int, string)> wordsWithIndentationLevel)
    {
        var alphabeticallyOrderedWords = wordsWithIndentationLevel.OrderBy(x => x.Item1).ThenBy(x => x.Item2);
        foreach(var word in alphabeticallyOrderedWords)
        {
            HandleIndentation(word.Item1);
            Console.WriteLine($"{StringFormatter.dash} {word.Item2}");
        }
    }
    private void HandleIndentation(int indentationLevel)
    {
        for(int i = 0; i < indentationLevel ; i++)
        {
            Console.Write(StringFormatter.spaceCharacter);
        }
    }
}
