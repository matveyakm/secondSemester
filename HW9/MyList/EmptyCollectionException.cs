// <copyright file="EmptyCollectionException.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace MyList;

/// <summary>
/// collection is empty.
/// </summary>
public class EmptyCollectionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyCollectionException"/> class.
    /// </summary>
    /// <param name="message">exception message.</param>
    public EmptyCollectionException(string message)
        : base(message)
    {
    }
}