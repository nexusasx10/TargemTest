using System;

namespace Calculator
{
    public class DelegateToken : IToken
    {
        private readonly Func<string, bool> recognizer;

        public string Type { get; }
        public int Priority { get; }

        public DelegateToken(string type, int priority, Func<string, bool> recognizer)
        {
            Type = type;
            Priority = priority;
            this.recognizer = recognizer;
        }

        public bool IsMatch(string input) => recognizer(input);
    }
}
