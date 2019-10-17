namespace Calculator
{
    public interface IToken
    {
        string Type { get; }
        int Priority { get; }

        bool IsMatch(string input);
    }
}
