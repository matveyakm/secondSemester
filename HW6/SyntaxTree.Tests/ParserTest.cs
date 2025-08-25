// <copyright file="ParserTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace SyntaxTree.Tests;

/// <summary>
/// tests for syntax tree.
/// </summary>
public class ParserTest
{
    /// <summary>
    /// calculate expression.
    /// </summary>
    [Test]
    public void SyntaxTree_Calculate()
    {
        var expression = Parser.ParseExpression("(* (+ (* 3 4) (/ 4 2)) (/ 10 2))");
        const int expected = 70;

        Assert.That(expression.Calculate(), Is.EqualTo(expected));
    }

    /// <summary>
    /// calculate single expression.
    /// </summary>
    [Test]
    public void SyntaxTree_Calculate_SingleExpression()
    {
        var expression = Parser.ParseExpression("197");
        const int expectedResult = 197;
        Assert.That(expression.Calculate(), Is.EqualTo(expectedResult));
    }

    /// <summary>
    /// parse string with expression.
    /// </summary>
    [Test]
    public void Parser_ParseExpression()
    {
        var expression = Parser.ParseExpression("(* 2 (* 8 (+ 1 3)))");
        const string expected = "(* 2 (* 8 (+ 1 3)))";

        Assert.That(expression.Print(), Is.EqualTo(expected));
    }

    /// <summary>
    /// parse expression from file.
    /// </summary>
    [Test]
    public void Parser_ParseExpressionFromFile()
    {
        var file = Path.Combine(AppContext.BaseDirectory, "TestFile.txt");
        const string expected = "(* 2 (* 8 (+ 1 3)))";

        Assert.That(Parser.ParseExpressionFromFile(file).Print(), Is.EqualTo(expected));
    }

    /// <summary>
    /// should throw format exception.
    /// </summary>
    [Test]
    public void Parser_ParseExpression_ShouldThrowFormatException()
        => Assert.Throws<FormatException>(() => Parser.ParseExpression("(* 1 (2 2))"));

    /// <summary>
    /// should throw file not found exception.
    /// </summary>
    [Test]
    public void Parser_ParseFromFile_ShouldThrowFileNotFoundException()
        => Assert.Throws<FileNotFoundException>(() => Parser.ParseExpressionFromFile("NotFoundFile.txt"));

    /// <summary>
    /// should throw divide by zero exception.
    /// </summary>
    [Test]
    public void SyntaxTree_Calculate_ShouldThrowDivideByZeroException()
       => Assert.Throws<DivideByZeroException>(() => Parser.ParseExpression("(/ 10 0)").Calculate());
}
