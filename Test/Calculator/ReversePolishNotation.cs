using Calculator.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calculator
{
    public class ReversePolishNotation
    {
        private readonly Dictionary<TokenType, int> _operatorPriority = new Dictionary<TokenType, int>
        {
            { TokenType.PlusOperator , 1 },
            { TokenType.MinusOperator, 1 },
            { TokenType.MultiplyOperator, 2 },
            { TokenType.DivideOperator, 2 },
            { TokenType.UnaryMinusOperator, 3 },
            { TokenType.UnaryPlusOperator, 3 },
            { TokenType.PowerOperator, 4 }
        };

        private readonly Dictionary<TokenType, TokenType> _unaryOperatorAliases = new Dictionary<TokenType, TokenType>
        {
            { TokenType.MinusOperator, TokenType.UnaryMinusOperator },
            { TokenType.PlusOperator, TokenType.UnaryPlusOperator }
        };

        private readonly Dictionary<TokenType, Func<float, float, float>> _binaryOperators = new Dictionary<TokenType, Func<float, float, float>>
        {
            { TokenType.PlusOperator , (a, b) => a + b },
            { TokenType.MinusOperator, (a, b) => a - b },
            { TokenType.MultiplyOperator, (a, b) => a * b },
            { TokenType.DivideOperator, (a, b) => a / b },
            { TokenType.PowerOperator, (a, b) => (float)Math.Pow(a, b) }
        };

        private readonly Dictionary<TokenType, Func<float, float>> _unaryOperators = new Dictionary<TokenType, Func<float, float>>
        {
            { TokenType.UnaryMinusOperator, a => -a },
            { TokenType.UnaryPlusOperator, a => a }
        };

        public float Calculate(IEnumerable<Lexeme> lexemes, string input, NumberFormatInfo numberFormat)
        {
            Stack<float> stack = new Stack<float>();

            IEnumerable<Lexeme> notation = HandleUnaryOperators(lexemes);
            notation = FromInfix(notation);

            foreach (Lexeme lexeme in notation)
            {
                if (lexeme.TokenType == TokenType.Number)
                {
                    stack.Push(float.Parse(input.Substring(lexeme.StartIndex, lexeme.Length), numberFormat));
                }
                else if (_unaryOperators.ContainsKey(lexeme.TokenType))
                {
                    if (stack.Count < 1)
                        throw new ReversePolishNotationException("Missing operand");

                    float operand = stack.Pop();
                    stack.Push(_unaryOperators[lexeme.TokenType](operand));
                }
                else if (_binaryOperators.ContainsKey(lexeme.TokenType))
                {
                    if (stack.Count < 2)
                        throw new ReversePolishNotationException("Missing operand");

                    float second = stack.Pop();
                    float first = stack.Pop();
                    stack.Push(_binaryOperators[lexeme.TokenType](first, second));
                }
            }

            if (stack.Count == 0)
                throw new ReversePolishNotationException("Missing operand");
            if (stack.Count > 1)
                throw new ReversePolishNotationException("Missing operator");
            return stack.Pop();
        }

        private IEnumerable<Lexeme> HandleUnaryOperators(IEnumerable<Lexeme> lexemes)
        {
            Lexeme? prevLexeme = null;

            foreach (Lexeme lexeme in lexemes)
            {
                bool missingFirstOperand = prevLexeme == null ||
                    prevLexeme.Value.TokenType == TokenType.PlusOperator ||
                    prevLexeme.Value.TokenType == TokenType.MinusOperator ||
                    prevLexeme.Value.TokenType == TokenType.UnaryMinusOperator ||
                    prevLexeme.Value.TokenType == TokenType.UnaryPlusOperator ||
                    prevLexeme.Value.TokenType == TokenType.MultiplyOperator ||
                    prevLexeme.Value.TokenType == TokenType.DivideOperator ||
                    prevLexeme.Value.TokenType == TokenType.OpenBracket;

                if (_unaryOperatorAliases.ContainsKey(lexeme.TokenType) && missingFirstOperand)
                    yield return new Lexeme(lexeme.StartIndex, lexeme.Length, _unaryOperatorAliases[lexeme.TokenType]);
                else
                    yield return lexeme;

                prevLexeme = lexeme;
            }
        }

        private IEnumerable<Lexeme> FromInfix(IEnumerable<Lexeme> lexemes)
        {
            Stack<Lexeme> stack = new Stack<Lexeme>();
            foreach (Lexeme lexeme in lexemes)
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
                    case TokenType.PowerOperator:
                        if (stack.Count > 0)
                        {
                            Lexeme stackTop = stack.Peek();
                            while (_operatorPriority.ContainsKey(stackTop.TokenType) && _operatorPriority[lexeme.TokenType] <= _operatorPriority[stackTop.TokenType])
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
                    case TokenType.UnaryPlusOperator:
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
                        {
                            throw new ReversePolishNotationException("Unexpected close bracket");
                        }
                        break;
                }
            }
            while (stack.Count > 0)
            {
                Lexeme lexem = stack.Pop();
                if (!_operatorPriority.ContainsKey(lexem.TokenType))
                    throw new ReversePolishNotationException("Missing close bracket");
                yield return lexem;
            }
        }
    }
}
