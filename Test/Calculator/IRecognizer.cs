using System.Collections.Generic;

namespace Calculator
{
    public interface IRecognizer
    {
        int GetLexemeLength(string input, List<Lexeme> previousLexems);
    }
}
