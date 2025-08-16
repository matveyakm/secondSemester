// <copyright file="NetworkTopologyTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace NetworkOptimizer.Tests;

/// <summary>
/// tests for network topology.
/// </summary>
public class NetworkTopologyTest
{
    /// <summary>
    /// test throws exception file not found.
    /// </summary>
    [Test]
    public void Graph_ReadFormNonexistentFile_ShouldThrowException()
       => Assert.Throws<FileNotFoundException>(() => new NetworkTopology("NonexistentFile.txt"));

    /// <summary>
    /// test method AddConnection by adding new connection.
    /// </summary>
    [Test]
    public void NetworkTopology_AddConnection_NewEdge_ShouldReturnTrue()
    {
        var graph = new NetworkTopology();
        const int expectedNeighbour = 15;
        const int expectedThroughput = 150;

        graph.AddConnection(3, 15, 150);

        Assert.Multiple(() =>
        {
            Assert.That(graph.Connections[3][0].Node, Is.EqualTo(expectedNeighbour));
            Assert.That(graph.Connections[3][0].Bandwidth, Is.EqualTo(expectedThroughput));
        });
    }

    /// <summary>
    /// test throws format exception while read data from file.
    /// </summary>
    [Test]
    public void Graph_ReadGraphFromFile_ShouldThrowFormatException()
        => Assert.Throws<FormatException>(() =>
            new NetworkTopology(Path.Combine(AppContext.BaseDirectory, "IncorrectFormatNetwork.txt")));

    /// <summary>
    /// test throws exception add already existing connection.
    /// </summary>
    [Test]
    public void NetworkTopology_Add_AlreadyExistingEdge_ShouldThrowException()
    {
        var graph = new NetworkTopology();

        graph.AddConnection(2, 5, 60);

        Assert.Throws<DuplicateLinkException>(() => graph.AddConnection(2, 5, 60));
    }
    
    /// <summary>
    /// test search max spanning tree.
    /// </summary>
    [Test]
    public void Graph_SearchMaxSpanningTree()
    {
        var graph = new NetworkTopology(Path.Combine(AppContext.BaseDirectory, "TestNetworkOptimizer.txt"));
        var tree = graph.GenerateOptimalTopology();
        var expectedResult = new NetworkTopology();

        expectedResult.AddConnection(1, 2, 2);
        expectedResult.AddConnection(2, 4, 100);
        expectedResult.AddConnection(4, 5, 15);

        Assert.That(CompareTopology(expectedResult, tree), Is.True);
    }

    private static bool CompareTopology(NetworkTopology net1, NetworkTopology net2)
    {
        if (net1.Connections.Count != net2.Connections.Count)
        {
            return false;
        }

        foreach (var (vertex, edges1) in net1.Connections)
        {
            if (!net2.Connections.TryGetValue(vertex, out var edges2))
            {
                return false;
            }

            if (edges1.Count != edges2.Count)
            {
                return false;
            }

            for (var i = 0; i < edges1.Count; i++)
            {
                if (edges1[i] != edges2[i])
                {
                    return false;
                }
            }
        }

        return true;
    }
}