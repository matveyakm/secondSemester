// <copyright file="Node.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace SyntaxTree;

/// <summary>
/// node of syntax tree.
/// </summary>
public abstract class Node
{
    /// <summary>
    /// to calculate node.
    /// </summary>
    /// <returns>result.</returns>
    public abstract int Calculate();

    /// <summary>
    /// to print node.
    /// </summary>
    /// <returns>string with node.</returns>
    public abstract string Print();
}