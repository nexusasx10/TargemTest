using System.Collections.Generic;

namespace Calculator
{
    public class UnaryOperatorToken : IToken
    {
        public string Type { get; }
        public int Priority { get; }
        public string Operator { get; }

        public UnaryOperatorToken(string type, string op, int priority)
        {
            Type = type;
            Priority = priority;
            Operator = op;
        }

        public bool IsMatch(string input, List<Lexeme> previousLexems)
        {
            return input == Operator && (previousLexems.Count == 0 || previousLexems[previousLexems.Count - 1].Token.Type.EndsWith("Operator") || previousLexems[previousLexems.Count - 1].Token.Type == "OpenBracket");
        }
    }
}
