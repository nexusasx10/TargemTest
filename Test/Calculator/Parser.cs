using System.Collections.Generic;

namespace Calculator
{
    public class Parser
    {
        private readonly List<(IRecognizer recognizer, TokenType tokenType, int priority)> recognizers;
        private readonly List<TokenType> insignificantTokenTypes;

        public Parser(List<TokenType> insignificantTokenTypes)
        {
            this.insignificantTokenTypes = insignificantTokenTypes;
            recognizers = new List<(IRecognizer recognizer, TokenType tokenType, int priority)>();
        }

        public void RegisterRecognizer(IRecognizer recognizer, TokenType tokenType, int priority)
        {
            recognizers.Add((recognizer, tokenType, priority));
        }

        public IEnumerable<Lexeme> Parse(string input)
        {
            var lexems = new List<Lexeme>();

            while (input.Length > 0)
            {
                var maxLexemeLength = 0;
                int bestRecognizerIndex = -1;
                for (var i = 0; i < recognizers.Count; i++)
                {
                    var lexemeLength = recognizers[i].recognizer.GetLexemeLength(input, lexems);
                    var recognezerBetterOnLength = lexemeLength > maxLexemeLength;
                    var recognezerBetterOnPriority = lexemeLength > 0
                        && lexemeLength == maxLexemeLength
                        && (bestRecognizerIndex == -1 || recognizers[i].priority > recognizers[bestRecognizerIndex].priority);

                    if (recognezerBetterOnLength || recognezerBetterOnPriority)
                    {
                        maxLexemeLength = lexemeLength;
                        bestRecognizerIndex = i;
                    }                    
                }

                if (maxLexemeLength > 0)
                {
                    if (!insignificantTokenTypes.Contains(recognizers[bestRecognizerIndex].tokenType))
                    {
                        var lexeme = new Lexeme(input.Substring(0, maxLexemeLength), recognizers[bestRecognizerIndex].tokenType);                        
                        lexems.Add(lexeme);
                        yield return lexeme;
                    }
                    input = input.Substring(maxLexemeLength);
                }
                else
                    throw new ParseException(input);
            }
        }
    }
}
