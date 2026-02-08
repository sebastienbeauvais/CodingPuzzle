using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace StringPuzzle;

public class AlphabeticFormatter : IFormatter
{
    
    internal (int, string) nestedWord = (0, string.Empty);
    internal int tempIndentation = -1;
    public void FormatString(string inputString)
    {
        var wordsWithIndentationLevel = new List<(int, string)>();
        int indentationLevel = -1;
        foreach (string word in inputString.Split())
        {
            // There is bug within this function. Not handling "(" or ")" correctly atm. potentially can string.Split on "(" or ")"
            var tuple = PrepareWordsForPrinting(word, indentationLevel);
            indentationLevel = tuple.Item1; // This is used to keep track of the global indentation level (maybe move to an internal attribute of the class)
            tuple.Item1 = word.Contains(')') ? tuple.Item1 = tempIndentation : tuple.Item1 = indentationLevel;
            wordsWithIndentationLevel.Add(tuple);
            if(nestedWord.Item2 != string.Empty)
            {
                tuple = PrepareWordsForPrinting(nestedWord.Item2, nestedWord.Item1++);
                indentationLevel = tuple.Item1;
                wordsWithIndentationLevel.Add(tuple);
                // Clear nested word incase another appears
                nestedWord = (0, string.Empty);
            }
        }
        PrintWordsInAlphabeticalOrder(wordsWithIndentationLevel);
        
    }
    private (int, string) PrepareWordsForPrinting(string word, int indentationLevel)
    {
        List<char> cleanedWord = new List<char>();
        // I needed a way to track if I am at the end of the word while iterating over with a foreach loop so characterIdx was born!
        // Personally I think they are more readible that a for loop where we would have word[i] representing a character
        int characterIdx = -1;
        foreach(char character in word)
        {
            characterIdx++;
            if(character == StringFormatter.leftParen && characterIdx == 0)
            {
                indentationLevel++;
            }
            else if(character == StringFormatter.rightParen)
            {
                indentationLevel = HandleRightParen(characterIdx, word, indentationLevel, cleanedWord);
            }
            else if(character == StringFormatter.comma)
            {
                continue;
            }
            else if(characterIdx > 0 && characterIdx < word.Length && character == StringFormatter.leftParen)
            {
                return HandleLeftParen(indentationLevel, word, cleanedWord);
            }
            else if(word.Contains(StringFormatter.rightParen) && characterIdx == word.Length-1)
            {
                return (indentationLevel, string.Join("", cleanedWord.ToArray()));
            }
            else
            {
                cleanedWord.Add(character);
            }
        }
        return (indentationLevel, string.Join("", cleanedWord.ToArray()));
    }
    private (int, string) HandleLeftParen(int indentationLevel, string word, List<char> cleanedWord)
    {
        int incrementCurrentIndentationLevel = indentationLevel+1; 
        nestedWord = (incrementCurrentIndentationLevel, word.Split('(').Last()); 
        // return the current cleand word
        return (indentationLevel, string.Join("", cleanedWord.ToArray()));
        
    }
    private int HandleRightParen(int characterIdx, string word, int indentationLevel, List<char> cleanedWord)
    {
        bool isLastRightParen = characterIdx == word.Length -1 || word[characterIdx + 1] != StringFormatter.rightParen;
        if (isLastRightParen)
        {
            int count = 0;
            int i = characterIdx;
            while (i >= 0 && word[i] == StringFormatter.rightParen)
            {
                count++;
                i--;
            }
            tempIndentation = GetTempIndentation(indentationLevel, characterIdx, word, cleanedWord);
            indentationLevel -= count;
        }
        return indentationLevel;
    }
    private int GetTempIndentation(int indentationLevel, int characterIdx, string word, List<char> cleanedWord)
    {
        if(characterIdx < word.Length)
        {
            tempIndentation = indentationLevel;
        }
        return tempIndentation;
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
