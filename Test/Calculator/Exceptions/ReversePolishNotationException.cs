using System;

namespace Calculator.Exceptions
{
    public class ReversePolishNotationException : Exception
    {
        public ReversePolishNotationException(string message) : base(message)
        {
        }
    }
}
