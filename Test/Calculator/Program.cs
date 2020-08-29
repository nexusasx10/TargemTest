using System;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            char floatSeparator = '.';

            if (args.Length > 0 && (args[0] == "--help" || args[0] == "-h"))
            {
                Console.WriteLine("Usage:\ncalculator EXPRESSION\ncalculator\n to run interactive mode.");
                return;
            }

            Calc calculator = new Calc();
            if (args.Length > 0)
            {
                calculator.TryCalculate(string.Concat(args), floatSeparator);
            }
            else
            {
                while (true)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine();
                    if (input.Length > 0)
                        calculator.TryCalculate(input, floatSeparator);
                    else
                        break;
                }
            }
        }
    }
}
