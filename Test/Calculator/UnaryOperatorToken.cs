using System.Collections.Generic;

namespace Calculator
{
    public class UnaryOperatorRecognizer : IRecognizer
    {
        public string Operator { get; }

        public UnaryOperatorRecognizer(string @operator)
        {
            Operator = @operator;
        }

        public int GetLexemeLength(string input, List<Lexeme> previousLexems)
        {
            if (!input.StartsWith(Operator))
                return 0;

            if (previousLexems.Count == 0)
                return Operator.Length;

            var prevLexeme = previousLexems[previousLexems.Count - 1];
            var missingFirstOperand = prevLexeme.TokenType == TokenType.PlusOperator
                || prevLexeme.TokenType == TokenType.MinusOperator
                || prevLexeme.TokenType == TokenType.UnaryMinusOperator
                || prevLexeme.TokenType == TokenType.MultiplyOperator
                || prevLexeme.TokenType == TokenType.DivideOperator
                || prevLexeme.TokenType == TokenType.OpenBracket;

            return missingFirstOperand ? Operator.Length : 0;
        }
    }
}
