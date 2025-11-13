// <copyright file="Divide.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace SyntaxTree;

/// <summary>
/// operation "divide".
/// </summary>
public class Divide : Operation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Divide"/> class.
    /// </summary>
    /// <param name="left">left child.</param>
    /// <param name="right">right child.</param>
    public Divide(Node left, Node right)
        : base(left, right)
    {
    }

    /// <summary>
    /// Gets symbol of operation.
    /// </summary>
    protected override char OperationSymbol => '/';

    /// <summary>
    /// to calculate expression.
    /// </summary>
    /// <returns>result.</returns>
    public override int Calculate()
    {
        var left = this.Left.Calculate();
        var right = this.Right.Calculate();

        if (left == 0)
        {
            throw new DivideByZeroException("Division by zero");
        }

        return left / right;
    }
}