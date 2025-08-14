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

    /// <summary>
    /// to search optimal topology.
    /// </summary>
    /// <returns>optimal network.</returns>
    public NetworkTopology GenerateOptimalTopology()
    {
        this.ValidateNetwork();

        var optimalNetwork = new NetworkTopology();
        var edgeQueue = new PriorityQueue<(int From, int To, int Bandwidth), int>(
            Comparer<int>.Create((x, y) => y.CompareTo(x)));
        var connectedNodes = new HashSet<int>();

        int initialNode = this.connections.Keys.First();
        connectedNodes.Add(initialNode);

        foreach (var link in this.connections[initialNode])
        {
            edgeQueue.Enqueue((initialNode, link.Node, link.Bandwidth), link.Bandwidth);
        }

        while (edgeQueue.Count > 0 && connectedNodes.Count < this.connections.Count)
        {
            var current = edgeQueue.Dequeue();

            if (connectedNodes.Contains(current.To))
            {
                continue;
            }

            optimalNetwork.AddConnection(current.From, current.To, current.Bandwidth);
            connectedNodes.Add(current.To);

            foreach (var neighbor in this.connections[current.To])
            {
                if (!connectedNodes.Contains(neighbor.Node))
                {
                    edgeQueue.Enqueue((current.To, neighbor.Node, neighbor.Bandwidth), neighbor.Bandwidth);
                }
            }
        }

        return optimalNetwork.GetSortedTopology();
    }

    /// <summary>
    /// to write network in file.
    /// </summary>
    /// <param name="outputPath">file to write.</param>
    public void SaveToFile(string outputPath)
    {
        using var writer = new StreamWriter(outputPath);

        foreach (var node in this.connections.Keys.OrderBy(k => k))
        {
            var links = this.connections[node]
                .OrderBy(l => l.Node)
                .Select(l => $"{l.Node} ({l.Bandwidth})");

            writer.WriteLine($"{node}: {string.Join(", ", links)}");
        }
    }

    /// <summary>
    /// to validate network.
    /// </summary>
    private void ValidateNetwork()
    {
        if (this.connections.Count == 0)
        {
            throw new EmptyNetworkException("Network configuration is empty");
        }

        if (!this.IsNetworkFullyConnected())
        {
            throw new DisconnectedNetworkException("Network topology is disconnected");
        }
    }

    /// <summary>
    /// to check is network fully connected.
    /// </summary>
    /// <returns>false if network isn't connected.</returns>
    private bool IsNetworkFullyConnected()
    {
        var visited = new HashSet<int>();
        var nodesToVisit = new Stack<int>();

        if (this.connections.Count == 0)
        {
            return false;
        }

        nodesToVisit.Push(this.connections.Keys.First());

        while (nodesToVisit.Count > 0)
        {
            var current = nodesToVisit.Pop();
            if (visited.Contains(current))
            {
                continue;
            }

            visited.Add(current);

            foreach (var neighbor in this.connections[current])
            {
                if (!visited.Contains(neighbor.Node))
                {
                    nodesToVisit.Push(neighbor.Node);
                }
            }
        }

        return visited.Count == this.connections.Count;
    }

    /// <summary>
    /// to parse node from file.
    /// </summary>
    /// <param name="nodeId">node number.</param>
    /// <param name="connectionsData">tmp data.</param>
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

    /// <summary>
    /// to sort topology by node A.
    /// </summary>
    /// <returns>sorted topology.</returns>
    private NetworkTopology GetSortedTopology()
    {
        var sorted = new NetworkTopology();

        foreach (var node in this.connections.Keys.OrderBy(k => k))
        {
            foreach (var link in this.connections[node].OrderBy(l => l.Node))
            {
                sorted.AddConnection(node, link.Node, link.Bandwidth);
            }
        }

        return sorted;
    }
}