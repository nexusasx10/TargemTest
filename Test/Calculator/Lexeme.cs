namespace Calculator
{
    public class Lexeme
    {
        public string Text { get; }
        public TokenType TokenType { get; }

        public Lexeme(string text, TokenType tokenType)
        {
            Text = text;
            TokenType = tokenType;
        }
    }
}
