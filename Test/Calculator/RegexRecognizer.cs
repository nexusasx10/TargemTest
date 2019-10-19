using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class RegexRecognizer : IRecognizer
    {
        public string Pattern { get; }

        public RegexRecognizer(string pattern)
        {
            Pattern = pattern;
        }

        public int GetLexemeLength(string input, List<Lexeme> previousLexems)
        {
            var matches = Regex.Matches(input, Pattern);
            Match longestMatch = null;
            foreach (Match match in matches)
                if (match.Success && match.Index == 0 && (longestMatch == null || match.Length > longestMatch.Length))
                    longestMatch = match;
            return longestMatch == null
                ? 0
                : longestMatch.Length;
        }
    }
}
