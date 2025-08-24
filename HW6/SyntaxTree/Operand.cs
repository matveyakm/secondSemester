// <copyright file="Operand.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace SyntaxTree;

/// <summary>
/// operand of expression.
/// </summary>
public class Operand : Node
{
    private int value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Operand"/> class.
    /// </summary>
    /// <param name="value">value of node.</param>
    public Operand(int value)
    {
        this.value = value;
    }

    /// <summary>
    /// to calculate expression.
    /// </summary>
    /// <returns>result.</returns>
    public override int Calculate()
        => this.value;

    /// <summary>
    /// to print expression.
    /// </summary>
    /// <returns>string with expression.</returns>
    public override string Print()
        => this.value.ToString();
}