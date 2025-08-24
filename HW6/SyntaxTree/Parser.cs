// <copyright file="Parser.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace SyntaxTree;

using System.Text;

/// <summary>
/// expression's parser.
/// </summary>
public static class Parser
{
    /// <summary>
    /// to parse syntax tree from file.
    /// </summary>
    /// <param name="filePath">file .</param>
    /// <returns>syntax tree's root.</returns>
    public static Node ParseExpression(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        using var reader = new StreamReader(filePath);
        return ParseNode(reader);
    }

    /// <summary>
    /// to read operation.
    /// </summary>
    /// <param name="reader">text reader.</param>
    /// <returns>operation.</returns>
    private static char ReadOperation(TextReader reader)
    {
        SkipSpace(reader);

        var operation = (char)reader.Read();

        return operation switch
        {
            '+' or '-' or '*' or '/' => operation,
            _ => throw new FormatException($"Unexpected operation '{operation}'"),
        };
    }

    /// <summary>
    /// to parse operand.
    /// </summary>
    /// <param name="reader">text reader.</param>
    /// <returns>parsed operand.</returns>
    private static Operand ParseOperand(TextReader reader)
    {
        SkipSpace(reader);
        var digits = new StringBuilder();

        while (char.IsDigit((char)reader.Peek()))
        {
            digits.Append((char)reader.Read());
        }

        if (digits.Length == 0)
        {
            throw new FormatException("Expected digits");
        }

        return new Operand(int.Parse(digits.ToString()));
    }

    /// <summary>
    /// to skip whitespaces.
    /// </summary>
    /// <param name="reader">expression.</param>
    private static void SkipSpace(TextReader reader)
    {
        while (char.IsWhiteSpace((char)reader.Peek()))
        {
            reader.Read();
        }
    }

    /// <summary>
    /// to parse node.
    /// </summary>
    /// <param name="reader">expression to parse.</param>
    /// <returns>parsed node.</returns>
    private static Node ParseNode(TextReader reader)
    {
        SkipSpace(reader);

        var nextChar = reader.Peek();
        if (nextChar == -1)
        {
            throw new FormatException("Unexpected end of stream");
        }

        var currentChar = (char)nextChar;

        switch (currentChar)
        {
            case '(':
                reader.Read();
                return ParseOperation(reader);

            case var _ when char.IsDigit(currentChar):
                return ParseOperand(reader);

            case '+':
            case '-':
            case '*':
            case '/':
                throw new FormatException($"Operator '{currentChar}' must be inside parentheses");

            default:
                throw new FormatException($"Unexpected character '{currentChar}'");
        }
    }

    /// <summary>
    /// to parse operation.
    /// </summary>
    /// <param name="reader">expression to parse.</param>
    /// <returns>parsed node.</returns>
    private static Node ParseOperation(TextReader reader)
    {
        var operation = ReadOperation(reader);
        var left = ParseNode(reader);
        var right = ParseNode(reader);

        SkipSpace(reader);
        if (reader.Read() != ')')
        {
            throw new FormatException("Missing closing parenthesis");
        }

        return operation switch
        {
            '+' => new Add(left, right),
            '-' => new Subtract(left, right),
            '*' => new Multiply(left, right),
            '/' => new Divide(left, right),
            _ => throw new FormatException($"Unknown operation '{operation}'"),
        };
    }


}