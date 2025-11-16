// <copyright file="CalculatorTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace Calculator.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Main class for tests of calculator
    /// </summary>
    [TestFixture]
    public class CalculatorTest
    {
        /// <summary>
        /// Execute scripts
        /// </summary>
        [Test]
        public void Add_PositiveNumbers_ReturnsExpectedResult()
        {
            string result = string.Empty;
            string expectedResult = string.Empty;
            bool isEqual;

            Tokenazer tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('+');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('+');
            result = tokenazer.GetResult();
            expectedResult = "10+";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 4+6+ should be 10+");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('+');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "10";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 4+6= should be 10");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('*');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('*');
            result = tokenazer.GetResult();
            expectedResult = "24*";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 4*6* should be 24*");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('1');
            tokenazer.AddSymbol('0');
            tokenazer.AddSymbol('-');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('-');
            result = tokenazer.GetResult();
            expectedResult = "4-";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 10-6- should be 4-");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('1');
            tokenazer.AddSymbol('0');
            tokenazer.AddSymbol('-');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "4";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 4-6= should be 4");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('*');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "24";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 4*6= should be 24");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('/');
            result = tokenazer.GetResult();
            expectedResult = "4/";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 24/6/ should be 4/");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "4";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 4/6= should be 4");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('0');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "Division by zero!";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 4/0= should be 'Division by zero!'");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('5');
            tokenazer.AddSymbol('^');
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('+');
            result = tokenazer.GetResult();
            expectedResult = "25+";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 5^2+ should be 25+");

            tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('5');
            tokenazer.AddSymbol('^');
            tokenazer.AddSymbol('2');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "25";
            isEqual = expectedResult.Equals(result);
            Assert.That(isEqual, "Fail : 5^2= should be 25");
        }
    }
}