// <copyright file="Operation.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace SyntaxTree;

/// <summary>
/// operation of expression.
/// </summary>
public abstract class Operation : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Operation"/> class.
    /// </summary>
    /// <param name="left">left operand.</param>
    /// <param name="right">right operand.</param>
    protected Operation(Node left, Node right)
    {
        this.Left = left;
        this.Right = right;
    }

    /// <summary>
    /// Gets left operand of operator.
    /// </summary>
    protected Node Left { get; }

    /// <summary>
    /// Gets right operand of operator.
    /// </summary>
    protected Node Right { get; }

    /// <summary>
    /// Gets operation.
    /// </summary>
    protected abstract char OperationSymbol { get; }

    /// <summary>
    /// to print expression.
    /// </summary>
    /// <returns>string with expression.</returns>
    public override string Print()
        => $"({this.OperationSymbol} {this.Left.Print()} {this.Right.Print()})";

    /// <summary>
    /// to calculate expression.
    /// </summary>
    /// <returns>result.</returns>
    public abstract override int Calculate();
}