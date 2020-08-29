using Calculator.Exceptions;
using Calculator.Recognizers;
using System.Collections.Generic;

namespace Calculator
{
    public class Parser
    {
        private readonly List<(IRecognizer recognizer, TokenType tokenType, int priority)> _recognizers;
        private readonly List<TokenType> _insignificantTokenTypes;

        public Parser(List<TokenType> insignificantTokenTypes)
        {
            _insignificantTokenTypes = insignificantTokenTypes;
            _recognizers = new List<(IRecognizer recognizer, TokenType tokenType, int priority)>();
        }

        public void RegisterRecognizer(IRecognizer recognizer, TokenType tokenType, int priority) =>
            _recognizers.Add((recognizer, tokenType, priority));

        public IEnumerable<Lexeme> Parse(string input)
        {
            int currentIndex = 0;

            while (currentIndex < input.Length)
            {
                int maxLexemeLength = 0;
                int bestRecognizerIndex = -1;
                for (int i = 0; i < _recognizers.Count; i++)
                {
                    int lexemeLength = _recognizers[i].recognizer.GetLexemeLength(input, currentIndex);
                    bool recognezerBetterOnLength = lexemeLength > maxLexemeLength;
                    bool recognezerBetterOnPriority = lexemeLength > 0 &&
                        lexemeLength == maxLexemeLength &&
                        (bestRecognizerIndex == -1 || _recognizers[i].priority > _recognizers[bestRecognizerIndex].priority);

                    if (recognezerBetterOnLength || recognezerBetterOnPriority)
                    {
                        maxLexemeLength = lexemeLength;
                        bestRecognizerIndex = i;
                    }                    
                }

                if (maxLexemeLength > 0)
                {
                    if (!_insignificantTokenTypes.Contains(_recognizers[bestRecognizerIndex].tokenType))
                    {
                        Lexeme lexeme = new Lexeme(currentIndex, maxLexemeLength, _recognizers[bestRecognizerIndex].tokenType);
                        yield return lexeme;
                    }
                    currentIndex += maxLexemeLength;
                }
                else
                {
                    throw new ParseException(currentIndex);
                }
            }
        }
    }
}
