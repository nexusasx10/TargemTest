using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class RegexToken : IToken
    {
        public string Type { get; }
        public string Pattern { get; }
        public int Priority { get; }

        public RegexToken(string type, string pattern, int priority)
        {
            Type = type;
            Pattern = pattern;
            Priority = priority;
        }

        public bool IsMatch(string input, List<Lexeme> previousLexems)
        {
            var matches = Regex.Matches(input, Pattern);
            foreach (var match in matches)
                if (match.ToString().Length == input.Length)
                    return true;
            return false;
        }
    }
}
