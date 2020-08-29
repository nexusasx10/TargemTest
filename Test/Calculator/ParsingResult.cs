using System.Collections.Generic;

namespace Calculator
{
    public class ParsingResult
    {
        public bool Error { get; }
        public int BadCharIndex;
        public IEnumerable<Lexeme> Result;
    }
}
