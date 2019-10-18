using System;

namespace Calculator
{
    public class RPNError : Exception
    {
        public RPNError(string message) : base(message)
        {
        }
    }
}
