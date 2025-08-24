// <copyright file="EmptyNetworkException.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace NetworkOptimizer;

/// <summary>
/// network topology is empty.
/// </summary>
public class EmptyNetworkException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyNetworkException"/> class.
    /// </summary>
    /// <param name="message">message of exception.</param>
    public EmptyNetworkException(string message)
        : base(message)
    {
    }
}