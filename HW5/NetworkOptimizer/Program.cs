// <copyright file="Program.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

using NetworkOptimizer;



var graph = new NetworkTopology(args[0]);

graph.ValidateNetwork();

Console.WriteLine("All okey!!");
