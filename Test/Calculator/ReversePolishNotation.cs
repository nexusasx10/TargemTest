using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calculator
{
    public class ReversePolishNotation
    {
        private readonly Dictionary<TokenType, int> operatorPriority = new Dictionary<TokenType, int>
        {
            { TokenType.PlusOperator , 1 },
            { TokenType.MinusOperator, 1 },
            { TokenType.MultiplyOperator, 2 },
            { TokenType.DivideOperator, 2 },
            { TokenType.UnaryMinusOperator, 3 }
        };

        private readonly Dictionary<TokenType, Func<float, float>> unaryOperators = new Dictionary<TokenType, Func<float, float>>
        {
            { TokenType.UnaryMinusOperator , n => -n }
        };

        private readonly Dictionary<TokenType, Func<float, float, float>> binaryOperators = new Dictionary<TokenType, Func<float, float, float>>
        {
            { TokenType.PlusOperator , (a, b) => a + b },
            { TokenType.MinusOperator, (a, b) => a - b },
            { TokenType.MultiplyOperator, (a, b) => a * b },
            { TokenType.DivideOperator, (a, b) => a / b }
        };

        public Lexeme Calculate(IEnumerable<Lexeme> lexemes)
        {
            var formatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };

            var stack = new Stack<Lexeme>();
            foreach (var lexeme in FromInfix(lexemes))
            {
                if (lexeme.TokenType == TokenType.Number)
                    stack.Push(lexeme);
                else if (unaryOperators.ContainsKey(lexeme.TokenType))
                {
                    if (stack.Count < 1)
                        throw new ReversePolishNotationException("Missing operand");

                    var operand = float.Parse(stack.Pop().Text, formatInfo);
                    stack.Push(new Lexeme(unaryOperators[lexeme.TokenType](operand).ToString(formatInfo), TokenType.Number));
                }
                else if (binaryOperators.ContainsKey(lexeme.TokenType))
                {
                    if (stack.Count < 2)
                        throw new ReversePolishNotationException("Missing operand");

                    var second = float.Parse(stack.Pop().Text, formatInfo);
                    var first = float.Parse(stack.Pop().Text, formatInfo);
                    stack.Push(new Lexeme(binaryOperators[lexeme.TokenType](first, second).ToString(formatInfo), TokenType.Number));
                }
            }

            if (stack.Count == 0)
                throw new ReversePolishNotationException("Missing operand");
            if (stack.Count > 1)
                throw new ReversePolishNotationException("Missing operator");
            return stack.Pop();
        }

        public IEnumerable<Lexeme> FromInfix(IEnumerable<Lexeme> lexemes)
        {
            var stack = new Stack<Lexeme>();
            foreach (var lexeme in lexemes)
            {
                switch (lexeme.TokenType)
                {
                    case TokenType.Number:
                        yield return lexeme;
                        break;
                    case TokenType.PlusOperator:
                    case TokenType.MinusOperator:
                    case TokenType.MultiplyOperator:
                    case TokenType.DivideOperator:
                        if (stack.Count > 0)
                        {
                            var stackTop = stack.Peek();
                            while (operatorPriority.ContainsKey(stackTop.TokenType) && operatorPriority[lexeme.TokenType] <= operatorPriority[stackTop.TokenType])
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
                    case TokenType.UnaryMinusOperator:
                        stack.Push(lexeme);
                        break;
                    case TokenType.OpenBracket:
                        stack.Push(lexeme);
                        break;
                    case TokenType.CloseBracket:
                        if (stack.Count > 0)
                        {
                            while (stack.Peek().TokenType != TokenType.OpenBracket)
                            {
                                yield return stack.Pop();
                                if (stack.Count == 0)
                                    throw new ReversePolishNotationException("Unexpected close bracket");
                            }
                            stack.Pop();
                        }
                        else
                            throw new ReversePolishNotationException("Unexpected close bracket");
                        break;
                }
            }
            while (stack.Count > 0)
            {
                var lexem = stack.Pop();
                if (!operatorPriority.ContainsKey(lexem.TokenType))
                    throw new ReversePolishNotationException("Missing close bracket");
                yield return lexem;
            }
        }
    }
}
