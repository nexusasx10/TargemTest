using Calculator;
using Calculator.Recognizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        private Parser _parser;

        [TestInitialize]
        public void TestInitialize()
        {
            _parser = ConfigureParser();
        }

        public Parser ConfigureParser()
        {
            Parser parser = new Parser(new List<TokenType> { TokenType.Space });
            parser.RegisterRecognizer(new WhitespaceRecognizer(), TokenType.Space, 0);
            parser.RegisterRecognizer(new TextRecognizer("+"), TokenType.PlusOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("-"), TokenType.MinusOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("*"), TokenType.MultiplyOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("/"), TokenType.DivideOperator, 0);
            parser.RegisterRecognizer(new TextRecognizer("("), TokenType.OpenBracket, 0);
            parser.RegisterRecognizer(new TextRecognizer(")"), TokenType.CloseBracket, 0);
            parser.RegisterRecognizer(new NumberRecognizer('.'), TokenType.Number, 0);
            return parser;
        }

        public void CalcTest(string input, float expectedOutput)
        {
            Assert.AreEqual(expectedOutput, new ReversePolishNotation().Calculate(_parser.Parse(input), input, new NumberFormatInfo { NumberDecimalSeparator = "." }));
        }

        [TestMethod]
        public void TestSum() =>
            CalcTest("3+4", 7);

        [TestMethod]
        public void TestWhitespace() =>
            CalcTest("3 + 4", 7);

        [TestMethod]
        public void TestDiff() =>
            CalcTest("12-4", 8);

        [TestMethod]
        public void TestNegDiff() =>
            CalcTest("4-12", -8);

        [TestMethod]
        public void TestNeg() =>
            CalcTest("-23", -23);

        [TestMethod]
        public void TestNegWhitespace() =>
            CalcTest("- 23", -23);

        [TestMethod]
        public void TestOperationOrder() =>
            CalcTest("2+2*2", 6);

        [TestMethod]
        public void TestOperationOrderSwitched() =>
            CalcTest("2*2+2", 6);

        [TestMethod]
        public void TestBrackets() =>
            CalcTest("(2+2)*2", 8);

        [TestMethod]
        public void TestBracketsSwitched() =>
            CalcTest("2*(2+2)", 8);

        [TestMethod]
        public void TestNegBrackets() =>
            CalcTest("-(4-12)", 8);

        [TestMethod]
        public void TestOperationOrderMultiple() =>
            CalcTest("(3 * 4) + (3 * 4) - (3 * 4)", 12);

        [TestMethod]
        public void TestOperationOrderMultipleSwitched() =>
            CalcTest("(3 * 4) - (3 * 4) + (3 * 4)", 12);

        [TestMethod]
        public void TestNestedBrackets() =>
            CalcTest("(2 + 1) * (2 * (3 - 5))", -12);

        [TestMethod]
        public void TestNestedBracketsMultiple() =>
            CalcTest("((((((((2+3))))))))", 5);

        [TestMethod]
        public void TestNestedBracketsMultiple2() =>
            CalcTest("(2 + (2 + (2 + 2)))", 8);

        [TestMethod]
        public void TestTestNestedBracketsMultipleNeg() =>
            CalcTest("(-2 + (2 + (2 + 2)))", 4);

        [TestMethod]
        public void TestDivision() =>
            CalcTest("3/4", 0.75f);

        [TestMethod]
        public void TestMultiplyDivisionOrder() =>
            CalcTest("4*3/2", 6);

        [TestMethod]
        public void TestDivisionOrder() =>
            CalcTest("10/2/5", 1);

        [TestMethod]
        public void TestFloatingPoint() =>
            CalcTest("3.5", 3.5f);

        [TestMethod]
        public void TestFloatingPointMultiply() =>
            CalcTest("3.5*2", 7);

        [TestMethod]
        public void TestFloatingPointNeg() =>
            CalcTest("-3.5", -3.5f);

        [TestMethod]
        public void TestFloatingPointOperationOrder() =>
            CalcTest("(2.5 + 1.5) * (2.5 * (3.5 - 5.5))", -20);

        [TestMethod]
        public void TestMultiplyNeg() =>
            CalcTest("3 * -1", -3);

        [TestMethod]
        public void TestAllOperations() =>
            CalcTest("((1.2 * -16 + 21) / 3.0 + 42 * (1 - 32.21)) + (12 / 3 / 4)", -1309.22f);
    }
}
