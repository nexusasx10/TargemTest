namespace Calculator.Recognizers
{
    public interface IRecognizer
    {
        int GetLexemeLength(string input, int startIndex);
    }
}
