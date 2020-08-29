namespace Calculator
{
    public struct Lexeme
    {
        public int StartIndex { get; }
        public int Length { get; }
        public TokenType TokenType { get; }

        public Lexeme(int startIndex, int length, TokenType tokenType)
        {
            StartIndex = startIndex;
            Length = length;
            TokenType = tokenType;
        }
    }
}
