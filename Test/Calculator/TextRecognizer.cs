using System.Collections.Generic;

namespace Calculator
{
    public class TextRecognizer : IRecognizer
    {
        public string Text { get; }

        public TextRecognizer(string text)
        {
            Text = text;
        }

        public int GetLexemeLength(string input, List<Lexeme> previousLexems)
        {
            return input.StartsWith(Text)
                ? Text.Length
                : 0;
        }
    }
}
