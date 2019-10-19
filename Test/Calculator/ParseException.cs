using System;

namespace Calculator
{
    public class ParseException : Exception
    {
        public string Symbol { get; }

        public ParseException(string symbol)
        {
            Symbol = symbol;
        }
    }
}
