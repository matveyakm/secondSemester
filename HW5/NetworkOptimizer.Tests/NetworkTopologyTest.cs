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
    
    

}