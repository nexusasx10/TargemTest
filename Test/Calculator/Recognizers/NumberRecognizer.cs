namespace Calculator.Recognizers
{
    public class NumberRecognizer : IRecognizer
    {
        private readonly char _separator;

        public NumberRecognizer(char separator)
        {
            _separator = separator;
        }

        public int GetLexemeLength(string input, int startIndex)
        {
            bool wasSeparator = false;
            int curIdx = startIndex;
            while (curIdx < input.Length)
            {
                if (input[curIdx] == _separator)
                {
                    if (wasSeparator)
                        break;

                    wasSeparator = true;
                }
                else if (!char.IsDigit(input[curIdx]))
                    break;

                curIdx++;
            }

            return curIdx - startIndex;
        }
    }
}
