using Calculator.Exceptions;
using Calculator.Recognizers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calculator
{
    public class Calc
    {
        public void TryCalculate(string expression, char floatSeparator)
        {
            Parser parser = new Parser(new List<TokenType> { TokenType.Space });
            parser.RegisterRecognizer(new WhitespaceRecognizer(), TokenType.Space, 0);
            parser.RegisterRecognizer(new TextRecognizer("+"), TokenType.PlusOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("-"), TokenType.MinusOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("*"), TokenType.MultiplyOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("/"), TokenType.DivideOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("^"), TokenType.PowerOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("("), TokenType.OpenBracket, 0);
            parser.RegisterRecognizer(new TextRecognizer(")"), TokenType.CloseBracket, 0);
            parser.RegisterRecognizer(new NumberRecognizer(floatSeparator), TokenType.Number, 0);
            
            try
            {
                IEnumerable<Lexeme> lexems = parser.Parse(expression);
                NumberFormatInfo numberFormat = new NumberFormatInfo { NumberDecimalSeparator = floatSeparator.ToString() };
                float result = new ReversePolishNotation().Calculate(lexems, expression, numberFormat);
                Console.WriteLine(result.ToString(numberFormat));
            }
            catch (ParseException exc)
            {
                Console.WriteLine($"Unexpected symbol \"{expression.Substring(exc.BadSymbolIndex)}\"");
            }
            catch (ReversePolishNotationException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
