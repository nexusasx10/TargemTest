using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Calc
    {
        public void Calculate(string expression)
        {
            var parser = new Parser(new List<TokenType> { TokenType.Space });
            parser.RegisterRecognizer(new RegexRecognizer(@"\s+"), TokenType.Space, 0);
            parser.RegisterRecognizer(new TextRecognizer("+"), TokenType.PlusOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("-"), TokenType.MinusOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("*"), TokenType.MultiplyOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("/"), TokenType.DivideOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("("), TokenType.OpenBracket, 0);
            parser.RegisterRecognizer(new TextRecognizer(")"), TokenType.CloseBracket, 0);
            parser.RegisterRecognizer(new UnaryOperatorRecognizer("-"), TokenType.UnaryMinusOperator, 1);
            parser.RegisterRecognizer(new RegexRecognizer(@"\d*\.?\d+"), TokenType.Number, 0);
            
            try
            {
                var lexems = parser.Parse(expression);
                Console.WriteLine(new ReversePolishNotation().Calculate(lexems).Text);
            }
            catch (ParseException exc)
            {
                Console.WriteLine($"Unexpected symbol '{exc.Symbol}'");
            }
            catch (ReversePolishNotationException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
