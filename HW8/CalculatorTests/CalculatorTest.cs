// <copyright file="CalculatorTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace Calculator.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Main class for tests of calculator.
    /// </summary>
    [TestFixture]
    public class CalculatorTest
    {
        /// <summary>
        /// Verifies that the sequence '4+6+' returns the intermediate result "10+".
        /// </summary>
        [Test]
        public void Add_4Plus6Plus_Returns10Plus()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('+');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('+');

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("10+"));
        }

        /// <summary>
        /// Verifies that the sequence '4+6' followed by <see cref="Tokenazer.Complete"/> returns "10".
        /// </summary>
        [Test]
        public void Add_4Plus6Complete_Returns10()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('+');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("10"));
        }

        /// <summary>
        /// Verifies that the sequence '4*6*' returns the intermediate result "24*".
        /// </summary>
        [Test]
        public void Multiply_4Times6Times_Returns24Times()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('*');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('*');

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("24*"));
        }

        /// <summary>
        /// Verifies that the sequence '10-6-' returns the intermediate result "4-".
        /// </summary>
        [Test]
        public void Subtract_10Minus6Minus_Returns4Minus()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('1');
            tokenazer.AddSymbol('0');
            tokenazer.AddSymbol('-');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('-');

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("4-"));
        }

        /// <summary>
        /// Verifies that the sequence '10-6' followed by <see cref="Tokenazer.Complete"/> returns "4".
        /// </summary>
        [Test]
        public void Subtract_10Minus6Complete_Returns4()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('1');
            tokenazer.AddSymbol('0');
            tokenazer.AddSymbol('-');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("4"));
        }

        /// <summary>
        /// Verifies that the sequence '4*6' followed by <see cref="Tokenazer.Complete"/> returns "24".
        /// </summary>
        [Test]
        public void Multiply_4Times6Complete_Returns24()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('*');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("24"));
        }

        /// <summary>
        /// Verifies that the sequence '24/6/' returns the intermediate result "4/".
        /// </summary>
        [Test]
        public void Divide_24Div6Div_Returns4Div()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('6');
            tokenazer.AddSymbol('/');

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("4/"));
        }

        /// <summary>
        /// Verifies that the sequence '24/6' followed by <see cref="Tokenazer.Complete"/> returns "4".
        /// </summary>
        [Test]
        public void Divide_24Div6Complete_Returns4()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('6');
            tokenazer.Complete();

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("4"));
        }

        /// <summary>
        /// Verifies that the sequence '4/0' followed by <see cref="Tokenazer.Complete"/> returns "Division by zero!".
        /// </summary>
        [Test]
        public void Divide_4Div0Complete_ReturnsDivisionByZeroMessage()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('4');
            tokenazer.AddSymbol('/');
            tokenazer.AddSymbol('0');
            tokenazer.Complete();

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("Division by zero!"));
        }

        /// <summary>
        /// Verifies that the sequence '5^2+' returns the intermediate result "25+".
        /// </summary>
        [Test]
        public void Power_5Pow2Plus_Returns25Plus()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('5');
            tokenazer.AddSymbol('^');
            tokenazer.AddSymbol('2');
            tokenazer.AddSymbol('+');

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("25+"));
        }

        /// <summary>
        /// Verifies that the sequence '5^2' followed by <see cref="Tokenazer.Complete"/> returns "25".
        /// </summary>
        [Test]
        public void Power_5Pow2Complete_Returns25()
        {
            var tokenazer = new Calculator.Tokenazer();
            tokenazer.AddSymbol('5');
            tokenazer.AddSymbol('^');
            tokenazer.AddSymbol('2');
            tokenazer.Complete();

            var result = tokenazer.GetResult();

            Assert.That(result, Is.EqualTo("25"));
        }
    }
}