using System;

namespace Calculator
{
    public class Calc
    {
        static void Main(string[] args)
        {
            var tokens = new IToken[]
            {
                new RegexToken("Space", @"\s+", 0),
                new RegexToken("PlusOperator", @"\+", 0),
                new RegexToken("MinusOperator", @"-", 0),
                new RegexToken("MultiplyOperator", @"\*", 0),
                new RegexToken("DivisionOperator", @"/", 0),
                new RegexToken("Number", @"\d*\.?\d+", 0),
                new RegexToken("OpenBracket", @"\(", 0),
                new RegexToken("CloseBracket", @"\)", 0),
            };

            var parser = new Parser(tokens);
            if (args.Length > 1)
            {
                var lexems = parser.Parse(args[1]);
                Console.WriteLine(new RPN().Calculate(lexems).Text);
            }
            else
            {
                var input = Console.ReadLine();
                while (input != string.Empty)
                {
                    try
                    {
                        var lexems = parser.Parse(input);
                        Console.WriteLine(new RPN().Calculate(lexems).Text);
                    }
                    catch (ParseError exc)
                    {
                        Console.WriteLine($"Unexpected symbol '{exc.Symbol}'");
                    }
                    catch (RPNError exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                    input = Console.ReadLine();
                }
            }
        }
    }
}
