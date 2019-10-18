using System;

namespace Calculator
{
    public class ParseError : Exception
    {
        public string Symbol { get; }

        public ParseError(string symbol)
        {
            Symbol = symbol;
        }
    }
}
