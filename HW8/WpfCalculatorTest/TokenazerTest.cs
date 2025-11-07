// <copyright file="TokenazerTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

using Xunit;
using WpfCalculator;

namespace WpfCalculatorTest
{
    public class TokenazerTest
    {

        public TokenazerTest()
        {
        }

        [Fact]
        public void Add_PositiveNumbers_ReturnsExpectedResult()
        {
            string result = string.Empty;
            string expectedResult = string.Empty;
            bool isEqual;

            Tokenazer tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('+');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('+');
            result = tokenazer.GetResult();
            expectedResult = "10+";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 4+6+ should be 10+");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('+');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "10";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 4+6= should be 10");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('*');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('*');
            result = tokenazer.GetResult();
            expectedResult = "24*";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 4*6* should be 24*");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('1');
            tokenazer.AddSymbol('0');
            tokenazer.AddSymbol('-');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('-');
            result = tokenazer.GetResult();
            expectedResult = "4-";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 10-6- should be 4-");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('1');
            tokenazer.AddSymbol('0');
            tokenazer.AddSymbol('-');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "4";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 4-6= should be 4");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('*');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "24";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 4*6= should be 24");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('/');
            result = tokenazer.GetResult();
            expectedResult = "4/";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 24/6/ should be 4/");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "4";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 4/6= should be 4");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('0');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "Division by zero!";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 4/0= should be 'Division by zero!'");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('5');
            tokenazer.AddSymbol('^');
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('+');
            result = tokenazer.GetResult();
            expectedResult = "25+";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 5^2+ should be 25+");

            tokenazer = new WpfCalculator.Tokenazer();
            tokenazer.AddSymbol('5');
            tokenazer.AddSymbol('^');
            tokenazer.AddSymbol('2');
            tokenazer.Complete();
            result = tokenazer.GetResult();
            expectedResult = "25";
            isEqual = expectedResult.Equals(result);
            Assert.True(isEqual, "Fail : 5^2= should be 25");
        }
    }
}
