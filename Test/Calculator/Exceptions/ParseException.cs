using System;

namespace Calculator.Exceptions
{
    public class ParseException : Exception
    {
        public int BadSymbolIndex { get; }

        public ParseException(int badSymbolIndex)
        {
            BadSymbolIndex = badSymbolIndex;
        }
    }
}
