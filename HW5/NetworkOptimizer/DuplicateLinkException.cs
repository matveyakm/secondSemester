// <copyright file="DuplicateLinkException.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace NetworkOptimizer;

/// <summary>
/// Connection already exists.
/// </summary>
public class DuplicateLinkException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateLinkException"/> class.
    /// </summary>
    /// <param name="message">message of exception.</param>
    public DuplicateLinkException(string message)
        : base(message)
    {
    }
}