using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        public Parser ConfigureParser()
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
            return parser;
        }

        [TestMethod]
        public void TestSum()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("3+4")).Text, "7");
        }

        [TestMethod]
        public void TestWhitespace()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("3 + 4")).Text, "7");
        }

        [TestMethod]
        public void TestDiff()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("12-4")).Text, "8");
        }

        [TestMethod]
        public void TestNegDiff()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("4-12")).Text, "-8");
        }

        [TestMethod]
        public void TestNeg()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("-23")).Text, "-23");
        }

        [TestMethod]
        public void TestNegWhitespace()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("- 23")).Text, "-23");
        }

        [TestMethod]
        public void TestOperationOrder()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("2+2*2")).Text, "6");
        }

        [TestMethod]
        public void TestOperationOrderSwitched()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("2*2+2")).Text, "6");
        }

        [TestMethod]
        public void TestBrackets()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("(2+2)*2")).Text, "8");
        }

        [TestMethod]
        public void TestBracketsSwitched()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("2*(2+2)")).Text, "8");
        }

        [TestMethod]
        public void TestNegBrackets()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("-(4-12)")).Text, "8");
        }

        [TestMethod]
        public void TestOperationOrderMultiple()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("(3 * 4) + (3 * 4) - (3 * 4)")).Text, "12");
        }

        [TestMethod]
        public void TestOperationOrderMultipleSwitched()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("(3 * 4) - (3 * 4) + (3 * 4)")).Text, "12");
        }

        [TestMethod]
        public void TestDobleNeg()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("--3")).Text, "3");
        }

        [TestMethod]
        public void TestTripleNeg()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("---3")).Text, "-3");
        }

        [TestMethod]
        public void TestNestedBrackets()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("(2 + 1) * (2 * (3 - 5))")).Text, "-12");
        }

        [TestMethod]
        public void TestNestedBracketsMultiple()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("((((((((2+3))))))))")).Text, "5");
        }

        [TestMethod]
        public void TestNestedBracketsMultiple2()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("(2 + (2 + (2 + 2)))")).Text, "8");
        }

        [TestMethod]
        public void TestTestNestedBracketsMultipleNeg()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("(-2 + (2 + (2 + 2)))")).Text, "4");
        }

        [TestMethod]
        public void TestDivision()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("3/4")).Text, "0.75");
        }

        [TestMethod]
        public void TestMultiplyDivisionOrder()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("4*3/2")).Text, "6");
        }

        [TestMethod]
        public void TestDivisionOrder()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("10/2/5")).Text, "1");
        }

        [TestMethod]
        public void TestFloatingPoint()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("3.5")).Text, "3.5");
        }

        [TestMethod]
        public void TestFloatingPointMultiply()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("3.5*2")).Text, "7");
        }

        [TestMethod]
        public void TestFloatingPointNeg()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("-3.5")).Text, "-3.5");
        }

        [TestMethod]
        public void TestFloatingPointOperationOrder()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("(2.5 + 1.5) * (2.5 * (3.5 - 5.5))")).Text, "-20");
        }

        [TestMethod]
        public void TestMultiplyNeg()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("3 * -1")).Text, "-3");
        }

        [TestMethod]
        public void TestAllOperations()
        {
            var parser = ConfigureParser();
            Assert.AreEqual(new ReversePolishNotation().Calculate(parser.Parse("((1.2 * -16 + 21) / 3.0 + 42 * (1 - 32.21)) + (12 / 3 / 4)")).Text, "-1309.22");
        }
    }
}
