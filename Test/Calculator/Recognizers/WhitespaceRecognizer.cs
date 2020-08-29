namespace Calculator.Recognizers
{
    public class WhitespaceRecognizer : IRecognizer
    {
        public int GetLexemeLength(string input, int startIndex)
        {
            int curIdx = startIndex;
            while (curIdx < input.Length && char.IsWhiteSpace(input[curIdx]))
                curIdx++;

            return curIdx - startIndex;
        }
    }
}
