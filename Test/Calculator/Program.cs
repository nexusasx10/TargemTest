using System;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0 && (args[0] == "--help" || args[0] == "-h"))
            {
                Console.WriteLine("Usage:\ncalculator EXPRESSION\ncalculator\n to run interactive mode.");
                return;
            }

            var calculator = new Calc();
            if (args.Length > 0)
            {
                calculator.Calculate(string.Concat(args));
            }
            else
            {
                while (true)
                {
                    Console.Write("> ");
                    var input = Console.ReadLine();
                    if (input.Length > 0)
                        calculator.Calculate(input);
                    else
                        break;
                }
            }
        }
    }
}
