namespace Calculator.Recognizers
{
    public class TextRecognizer : IRecognizer
    {
        public string Text { get; }

        public TextRecognizer(string text)
        {
            Text = text;
        }

        public int GetLexemeLength(string input, int startIndex)
        {
            int curIdx = startIndex;

            while (curIdx < input.Length &&
                curIdx - startIndex < Text.Length &&
                Text[curIdx - startIndex] == input[curIdx])
            {
                curIdx++;
            }

            return curIdx - startIndex == Text.Length
                ? Text.Length
                : 0;
        }
    }
}
