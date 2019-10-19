using System;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var calculator = new Calc();
            if (args.Length > 1)
            {
                calculator.Calculate(args[1]);
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
