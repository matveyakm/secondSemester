// <copyright file="DisconnectedNetworkException.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace NetworkOptimizer;

/// <summary>
/// network topology is disconnected.
/// </summary>
public class DisconnectedNetworkException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DisconnectedNetworkException"/> class.
    /// </summary>
    /// <param name="message">message of exception.</param>
    public DisconnectedNetworkException(string message)
        : base(message)
    {
    }
}