using System;
using System.Collections.Generic;
using List;

namespace Calculator
{
    public class Calc
    {
        static void Main(string[] args)
        {
            var list = new MyList<int>();
            Console.WriteLine(list.Count);
            list.Add(1);
            Console.WriteLine(list.Count);
            list.Add(2);
            Console.WriteLine(list.Count);
            list.Add(3);
            Console.WriteLine(list.Count);
            list.Clear();
            Console.WriteLine(list.Count);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine();
            Console.WriteLine(list.Contains(1));
            Console.WriteLine(list.Contains(2));
            Console.WriteLine(list.Contains(3));
            Console.WriteLine(list.Contains(4));

            Console.WriteLine();
            foreach (var item in list)
                Console.WriteLine(item);

            Console.WriteLine();
            for (var i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);

            Console.WriteLine();
            Console.WriteLine(list.IndexOf(2));

            Console.WriteLine();
            list.Insert(1, 4);
            foreach (var item in list)
                Console.WriteLine(item);

            Console.WriteLine();
            list.RemoveAt(2);
            foreach (var item in list)
                Console.WriteLine(item);

            Console.WriteLine();
            var a = new int[5];
            list.CopyTo(a, 1);

            Console.WriteLine();
            foreach (var item in list)
                Console.WriteLine(item);

            foreach (var item in a)
                Console.WriteLine(item);

            var tokens = new IToken[]
            {
                new RegexToken("Space", @"\s+", 0),
                new RegexToken("PlusOperator", @"\+", 0),
                new RegexToken("MinusOperator", @"-", 0),
                new UnaryOperatorToken("UnaryMinusOperator", "-", 1),
                new RegexToken("MultiplyOperator", @"\*", 0),
                new RegexToken("DivisionOperator", @"/", 0),
                new RegexToken("Number", @"\d*\.?\d+", 0),
                new RegexToken("OpenBracket", @"\(", 0),
                new RegexToken("CloseBracket", @"\)", 0),
            };

            var parser = new Parser(tokens, new List<string> { "Space" });

            
            try
            {
                Console.WriteLine(new RPN().Calculate(parser.Parse("3+4")).Text == "7");
                Console.WriteLine(new RPN().Calculate(parser.Parse("3 + 4")).Text == "7");
                Console.WriteLine(new RPN().Calculate(parser.Parse("12-4")).Text == "8");
                Console.WriteLine(new RPN().Calculate(parser.Parse("4-12")).Text == "-8");
                Console.WriteLine(new RPN().Calculate(parser.Parse("-23")).Text == "-23");
                Console.WriteLine(new RPN().Calculate(parser.Parse("- 23")).Text == "-23");
                Console.WriteLine(new RPN().Calculate(parser.Parse("2 * 2 + 2")).Text == "6");
                Console.WriteLine(new RPN().Calculate(parser.Parse("2 + 2 * 2")).Text == "6");
                Console.WriteLine(new RPN().Calculate(parser.Parse("(2 + 2) * 2")).Text == "8");
                Console.WriteLine(new RPN().Calculate(parser.Parse("-(4 - 12)")).Text == "8");
                Console.WriteLine(new RPN().Calculate(parser.Parse("(3*4)+(3*4)-(3*4)")).Text == "12");
                Console.WriteLine(new RPN().Calculate(parser.Parse("(3*4)-(3*4)+(3*4)")).Text == "12");
                Console.WriteLine(new RPN().Calculate(parser.Parse("--3")).Text == "3");
                Console.WriteLine(new RPN().Calculate(parser.Parse("---3")).Text == "-3");
                Console.WriteLine(new RPN().Calculate(parser.Parse("(2 + 1) * (2 * (3 - 5))")).Text == "-12");
                Console.WriteLine(new RPN().Calculate(parser.Parse("((((((((2+3))))))))")).Text == "5");
                Console.WriteLine(new RPN().Calculate(parser.Parse("(2 + (2 + (2 + 2)))")).Text == "8");
                Console.WriteLine(new RPN().Calculate(parser.Parse("(-2 + (2 + (2 + 2)))")).Text == "4");
                Console.WriteLine(new RPN().Calculate(parser.Parse("3/4")).Text == "0.75");
                Console.WriteLine(new RPN().Calculate(parser.Parse("4*3/2")).Text == "6");
                Console.WriteLine(new RPN().Calculate(parser.Parse("10/2/5")).Text == "1");
                Console.WriteLine(new RPN().Calculate(parser.Parse("3.5")).Text == "3.5");
                Console.WriteLine(new RPN().Calculate(parser.Parse("3.5 * 2")).Text == "7");
                Console.WriteLine(new RPN().Calculate(parser.Parse("-3.5")).Text == "-3.5");
                Console.WriteLine(new RPN().Calculate(parser.Parse("(2.5 + 1.5) * (2.5 * (3.5 - 5.5))")).Text == "-20");
                Console.WriteLine(new RPN().Calculate(parser.Parse("3 * -1")).Text == "-3");
            }
            catch (ParseError exc)
            {
                Console.WriteLine($"Unexpected symbol '{exc.Symbol}'");
            }
            catch (RPNError exc)
            {
                Console.WriteLine(exc.Message);
            }


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
