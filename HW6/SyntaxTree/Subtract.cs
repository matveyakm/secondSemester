// <copyright file="Subtract.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace SyntaxTree;

/// <summary>
/// operation "subtract".
/// </summary>
public class Subtract : Operation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Subtract"/> class.
    /// </summary>
    /// <param name="left">left child.</param>
    /// <param name="right">right child.</param>
    public Subtract(Node left, Node right)
        : base(left, right)
    {
    }

    /// <summary>
    /// Gets operation.
    /// </summary>
    protected override char OperationSymbol => '-';

    /// <summary>
    /// to calculate expression.
    /// </summary>
    /// <returns>result.</returns>
    public override int Calculate()
        => this.Left.Calculate() - this.Right.Calculate();
}