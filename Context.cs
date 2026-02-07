using System;

namespace StringPuzzle;

public class Context
{
    private IFormatter? _formatter;
    internal void SetStrategy(IFormatter formatter)
    {
        _formatter = formatter;
    }
    internal void ExecuteFormatting(string input)
    {
        if(_formatter == null)
        {
            throw new InvalidOperationException("Strategy not set.");
        }
        _formatter.FormatString(input);
    }
}
