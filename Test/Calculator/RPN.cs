using System.Collections.Generic;
using System.Globalization;

namespace Calculator
{
    public class RPN
    {
        private Dictionary<string, int> priority = new Dictionary<string, int>
        {
            {"PlusOperator", 1 },
            {"MinusOperator", 1 },
            {"MultiplyOperator", 2 },
            {"DivisionOperator", 2 },
        };

        public Lexeme Calculate(IEnumerable<Lexeme> lexemes)
        {
            var formatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };

            var stack = new Stack<Lexeme>();
            foreach (var lexeme in FromInfix(lexemes))
            {
                switch (lexeme.Token.Type)
                {
                    case "Number":
                        stack.Push(lexeme);
                        break;
                    case "PlusOperator":
                        if (stack.Count < 2)
                            throw new RPNError("Missing operand");
                        var second = float.Parse(stack.Pop().Text, formatInfo);
                        var first = float.Parse(stack.Pop().Text, formatInfo);
                        stack.Push(new Lexeme($"{first + second}", null, 0));
                        break;
                    case "MinusOperator":
                        if (stack.Count < 2)
                            throw new RPNError("Missing operand");
                        second = float.Parse(stack.Pop().Text, formatInfo);
                        first = float.Parse(stack.Pop().Text, formatInfo);
                        stack.Push(new Lexeme($"{first - second}", null, 0));
                        break;
                    case "MultiplyOperator":
                        if (stack.Count < 2)
                            throw new RPNError("Missing operand");
                        second = float.Parse(stack.Pop().Text, formatInfo);
                        first = float.Parse(stack.Pop().Text, formatInfo);
                        stack.Push(new Lexeme($"{first * second}", null, 0));
                        break;
                    case "DivisionOperator":
                        if (stack.Count < 2)
                            throw new RPNError("Missing operand");
                        second = float.Parse(stack.Pop().Text, formatInfo);
                        first = float.Parse(stack.Pop().Text, formatInfo);
                        stack.Push(new Lexeme($"{first / second}", null, 0));
                        break;
                }
            }

            if (stack.Count == 0)
                throw new RPNError("Missing operand");
            if (stack.Count > 1)
                throw new RPNError("Missing operator");
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
                        if (stack.Count > 0)
                        {
                            var stackTop = stack.Peek();
                            while (priority.ContainsKey(stackTop.Token.Type) && priority[lexeme.Token.Type] < priority[stackTop.Token.Type])
                            {
                                yield return stack.Pop();
                                if (stack.Count > 0)
                                    stackTop = stack.Peek();
                                else
                                    break;
                            }
                        }
                        stack.Push(lexeme);
                        break;
                    case "OpenBracket":
                        stack.Push(lexeme);
                        break;
                    case "CloseBracket":
                        if (stack.Count > 0)
                        {
                            while (stack.Peek().Token.Type != "OpenBracket")
                            {
                                yield return stack.Pop();
                                if (stack.Count == 0)
                                    throw new RPNError("Unexpected close bracket");
                            }
                            stack.Pop();
                        }
                        else
                            throw new RPNError("Unexpected close bracket");
                        break;
                }
            }
            while (stack.Count > 0)
            {
                var lexem = stack.Pop();
                if (!priority.ContainsKey(lexem.Token.Type))
                    throw new RPNError("Missing close bracket");
                yield return lexem;
            }
        }
    }
}
