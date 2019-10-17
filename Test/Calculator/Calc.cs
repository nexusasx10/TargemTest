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
                new DelegateToken("Number", 0, input => float.TryParse(input, out _)),
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
                    var lexems = parser.Parse(input);
                    foreach (var lexem in lexems)
                        Console.WriteLine($"{lexem.Token.Type} {lexem.Text}");
                    Console.WriteLine(new RPN().Calculate(lexems).Text);
                    input = Console.ReadLine();
                }
            }
        }
    }
}
