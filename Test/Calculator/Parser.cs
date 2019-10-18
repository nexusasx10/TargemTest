using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Parser
    {
        private IEnumerable<IToken> tokens;
        private List<string> insignificantTokenTypes;

        public Parser(IEnumerable<IToken> tokens, List<string> insignificantTokenTypes)
        {
            this.tokens = tokens;
            this.insignificantTokenTypes = insignificantTokenTypes;
        }

        public IEnumerable<Lexeme> Parse(string input)
        {
            var lexems = new List<Lexeme>();

            for (var start = 0; start < input.Length; start++)
            {
                IToken matchedToken = null;
                var matchedStart = 0;
                var matchedLength = 0;
                for (var length = input.Length - start; length > 0; length--)
                {
                    foreach (var token in tokens)
                    {
                        if (token.IsMatch(input.Substring(start, length), lexems) && (matchedToken == null || matchedToken.Priority < token.Priority))
                        {
                            matchedToken = token;
                            matchedStart = start;
                            matchedLength = length;
                        }
                    }

                    if (matchedToken != null)
                    {
                        var lexeme = new Lexeme(input.Substring(matchedStart, matchedLength), matchedToken, matchedStart);
                        if (!insignificantTokenTypes.Contains(matchedToken.Type))
                        {
                            lexems.Add(lexeme);
                            yield return lexeme;
                        }
                        start = matchedStart + matchedLength - 1;
                        break;
                    }
                }

                if (matchedToken == null)
                    throw new ParseError(input.Substring(start));
            }
        }
    }
}
