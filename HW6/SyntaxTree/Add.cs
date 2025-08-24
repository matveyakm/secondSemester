// <copyright file="Add.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace SyntaxTree;

/// <summary>
/// operation "add".
/// </summary>
public class Add : Operation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Add"/> class.
    /// </summary>
    /// <param name="left">left child.</param>
    /// <param name="right">right child.</param>
    public Add(Node left, Node right)
        : base(left, right)
    {
    }

    /// <summary>
    /// Gets operation.
    /// </summary>
    protected override char OperationSymbol => '+';

    /// <summary>
    /// to calculate expression.
    /// </summary>
    /// <returns>result.</returns>
    public override int Calculate()
        => this.Left.Calculate() + this.Right.Calculate();
}