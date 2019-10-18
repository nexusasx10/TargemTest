namespace Calculator
{
    public class Lexeme
    {
        public int Position { get; }
        public string Text { get; }
        public IToken Token { get; }
        public int Length => Text.Length;

        public Lexeme(string text, IToken token, int position)
        {
            Text = text;
            Token = token;
            Position = position;
        }
    }
}
