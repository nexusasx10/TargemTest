using System.Collections.Generic;

namespace Calculator
{
    public class Parser
    {
        private IEnumerable<IToken> tokens;

        public Parser(IEnumerable<IToken> tokens)
        {
            this.tokens = tokens;
        }

        public IEnumerable<Lexeme> Parse(string input)
        {
            for (var start = 0; start < input.Length - 1; start++)
            {
                IToken matchedToken = null;
                var matchedStart = 0;
                var matchedLength = 0;
                for (var length = input.Length - start; length > 0; length--)
                {
                    foreach (var token in tokens)
                    {
                        if (token.IsMatch(input.Substring(start, length)) && (matchedToken == null || matchedToken.Priority < token.Priority))
                        {
                            matchedToken = token;
                            matchedStart = start;
                            matchedLength = length;
                        }
                    }

                    if (matchedToken != null)
                    {
                        yield return new Lexeme(input.Substring(matchedStart, matchedLength), matchedToken);
                        start = matchedStart + matchedLength;
                        continue;
                    }
                }

                if (matchedToken == null)
                    throw new ParseError();
            }
        }
    }
}
