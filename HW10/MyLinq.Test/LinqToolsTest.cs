// <copyright file="LinqToolsTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LinqTools.Test;

using MyLinq;

/// <summary>
/// tests for LinqTools.
/// </summary>
public class LinqToolsTest
{
    /// <summary>
    /// test method GetPrimes by taking first 8 numbers.
    /// </summary>
    [Test]
    public void LinqTools_GetPrimes_FirstEightNumbers()
        => Assert.That(LinqTools.GetPrimes().Take(8), Is.EquivalentTo((int[])[2, 3, 5, 7, 11, 13, 17, 19]));

    /// <summary>
    /// test method Take by taking first 3 elements of string array.
    /// </summary>
    [Test]
    public void LinqTools_Take_FirstThreeElements()
    {
        Assert.That(
            ((string[])["matmeh", "matvey", "akimenko", "tp24", "university", "cool"]).Take(3),
            Is.EquivalentTo((string[])["matmeh", "matvey", "akimenko"]));
    }

    /// <summary>
    /// test method Skip by skipping first 3 numbers.
    /// </summary>
    [Test]
    public void LinqTools_Skip_SkipThreeElements()
    {
        int[] numbers = [1, 2, 3, 4, 5, 6, 7];

        Assert.That(numbers.Skip(3), Is.EquivalentTo((int[])[4, 5, 6, 7]));
    }

    /// <summary>
    /// test method take and skip by getting 5 elements with skipping first 3 numbers.
    /// </summary>
    [Test]
    public void LinqTools_SkipAndTakeCombination()
    {
        var primeNumbers = LinqTools.GetPrimes();
        Assert.That(primeNumbers.Skip(3).Take(5), Is.EquivalentTo((int[])[7, 11, 13, 17, 19]));
    }
}