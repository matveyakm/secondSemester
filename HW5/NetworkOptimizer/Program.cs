// <copyright file="Program.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

using NetworkOptimizer;

try
{
    var graph = new NetworkTopology(args[0]);
    var optimalTopology = graph.GenerateOptimalTopology();
    optimalTopology.SaveToFile(args[1]);
}
catch (Exception ex) when (ex is FileNotFoundException
                               or DirectoryNotFoundException
                               or DisconnectedNetworkException
                               or DuplicateLinkException or EmptyNetworkException or FormatException)
{
    Console.WriteLine(ex);
    return -1;
}

return 0;