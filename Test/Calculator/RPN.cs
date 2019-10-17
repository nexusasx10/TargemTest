using System.Collections.Generic;

namespace Calculator
{
    public class RPN
    {
        private Dictionary<string, int> priority = new Dictionary<string, int>
        {
            {"OpenBracket", 0 },
            {"CloseBracket", 0 },
            {"PlusOperator", 1 },
            {"MinusOperator", 1 },
            {"MultiplyOperator", 2 },
            {"DivisionOperator", 2 },
        };

        public Lexeme Calculate(IEnumerable<Lexeme> lexemes)
        {
            var stack = new Stack<Lexeme>();
            foreach (var lexeme in FromInfix(lexemes))
            {
                switch (lexeme.Token.Type)
                {
                    case "Number":
                        stack.Push(lexeme);
                        break;
                    case "PlusOperator":
                        stack.Push(new Lexeme($"{float.Parse(stack.Pop().Text) + float.Parse(stack.Pop().Text)}", null));
                        break;
                    case "MinusOperator":
                        stack.Push(new Lexeme($"{float.Parse(stack.Pop().Text) - float.Parse(stack.Pop().Text)}", null));
                        break;
                    case "MultiplyOperator":
                        stack.Push(new Lexeme($"{float.Parse(stack.Pop().Text) * float.Parse(stack.Pop().Text)}", null));
                        break;
                    case "DivisionOperator":
                        stack.Push(new Lexeme($"{float.Parse(stack.Pop().Text) / float.Parse(stack.Pop().Text)}", null));
                        break;
                }
            }
            return stack.Pop();
        }

        public IEnumerable<Lexeme> FromInfix(IEnumerable<Lexeme> lexemes)
        {
            var stack = new Stack<Lexeme>();
            foreach (var lexeme in lexemes)
            {
                switch (lexeme.Token.Type)
                {
                    case "Space":
                        break;
                    case "Number":
                        yield return lexeme;
                        break;
                    case "PlusOperator":
                    case "MinusOperator":
                    case "MultiplyOperator":
                    case "DivisionOperator":
                        var stackTop = stack.Peek();
                        if (priority.ContainsKey(stackTop.Token.Type) && priority[lexeme.Token.Type] < priority[stackTop.Token.Type])
                            yield return stack.Pop();
                        stack.Push(lexeme);
                        break;
                    case "OpenBracket":
                        stack.Push(lexeme);
                        break;
                    case "CloseBracket":
                        while (stack.Peek().Token.Type != "OpenBracket")
                            yield return stack.Pop();
                        stack.Pop();
                        break;
                }
            }
            while (stack.Count > 0)
                yield return stack.Pop();
        }
    }
}
