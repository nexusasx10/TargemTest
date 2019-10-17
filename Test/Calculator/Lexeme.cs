namespace Calculator
{
    public class Lexeme
    {
        public string Text { get; }
        public IToken Token { get; }

        public Lexeme(string text, IToken token)
        {
            Text = text;
            Token = token;
        }
    }
}
