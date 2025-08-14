// <copyright file="NetworkTopology.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace NetworkOptimizer;

/// <summary>
/// to represent network topology.
/// </summary>
public class NetworkTopology
{
    private readonly Dictionary<int, List<(int Node, int Bandwidth)>> connections;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkTopology"/> class.
    /// </summary>
    public NetworkTopology()
    {
        this.connections = new Dictionary<int, List<(int, int)>>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkTopology"/> class.
    /// </summary>
    /// <param name="inputFile">input file.</param>
    public NetworkTopology(string inputFile)
        : this()
        => this.ReadTopologyFromFile(inputFile);

    /// <summary>
    /// to read topology network from file.
    /// </summary>
    /// <param name="inputFile">file with topology network.</param>
    public void ReadTopologyFromFile(string inputFile)
    {
        if (!File.Exists(inputFile))
        {
            throw new FileNotFoundException("File not found", inputFile);
        }

        foreach (var line in File.ReadLines(inputFile))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(':', 2);
            if (parts.Length != 2)
            {
                throw new FormatException($"Invalid line format: {line}");
            }

            if (!int.TryParse(parts[0].Trim(), out var nodeId))
            {
                throw new FormatException($"Invalid node ID: {parts[0]}");
            }

            this.ProcessNodeConnections(nodeId, parts[1]);
        }
    }

    /// <summary>
    /// add connection to topology.
    /// </summary>
    /// <param name="nodeA">from.</param>
    /// <param name="nodeB">to.</param>
    /// <param name="bandwidth">throughput capacity between nodes.</param>
    public void AddConnection(int nodeA, int nodeB, int bandwidth)
    {
        if (!this.connections.ContainsKey(nodeA))
        {
            this.connections[nodeA] = (List<(int, int)>)[];
        }

        if (this.connections[nodeA].Any(x => x.Node == nodeB))
        {
            throw new DuplicateLinkException($"Connection {nodeA}-{nodeB} already exists");
        }

        this.connections[nodeA].Add((nodeB, bandwidth));
    }

    private void ProcessNodeConnections(int nodeId, string connectionsData)
    {
        foreach (var connection in connectionsData.Split(','))
        {
            var trimmed = connection.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                continue;
            }

            var linkInfo = trimmed.Split([' ', '(', ')'], StringSplitOptions.RemoveEmptyEntries);
            if (linkInfo.Length != 2 ||
                !int.TryParse(linkInfo[0], out var targetNode) ||
                !int.TryParse(linkInfo[1], out var bandwidth))
            {
                throw new FormatException($"Invalid connection format: {trimmed}");
            }

            this.AddConnection(nodeId, targetNode, bandwidth);
            this.AddConnection(targetNode, nodeId, bandwidth);
        }
    }
}