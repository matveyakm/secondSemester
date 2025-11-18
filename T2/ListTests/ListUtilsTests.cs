// <copyright file="ListUtilsTests.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace MyList.Tests;

using System;
using System.Collections.Generic;
using List;
using NUnit.Framework;

/// <summary>
/// Contains unit tests for the <see cref="ListUtils.Sort{T}"/> extension method.
/// </summary>
[TestFixture]
public class ListUtilsTests
{
    /// <summary>
    /// Verifies that Sort with default comparer sorts integers in ascending order.
    /// </summary>
    [Test]
    public void Sort_DefaultComparer_SortsIntegersAscending()
    {
        // Arrange
        var list = new MyList<int> { 64, 34, 25, 12, 22, 11, 90 };

        // Act
        list.Sort();

        // Assert
        var expected = new[] { 11, 12, 22, 25, 34, 64, 90 };
        Assert.That(list, Is.EqualTo(expected));
    }

    /// <summary>
    /// Verifies that Sort with custom descending comparer works correctly.
    /// </summary>
    [Test]
    public void Sort_DescendingComparer_SortsCorrectly()
    {
        // Arrange
        var list = new MyList<int> { 5, 2, 8, 1, 9 };
        var descendingComparer = Comparer<int>.Create((x, y) => y.CompareTo(x));

        // Act
        list.Sort(descendingComparer);

        // Assert
        Assert.That(list, Is.EqualTo(new[] { 9, 8, 5, 2, 1 }));
    }

    /// <summary>
    /// Verifies that Sort works correctly with string case-insensitive comparison.
    /// </summary>
    [Test]
    public void Sort_StringIgnoreCase_SortsAlphabeticallyIgnoringCase()
    {
        // Arrange
        var list = new MyList<string> { "banana", "Apple", "cherry", "ZEBRA" };

        // Act
        list.Sort(StringComparer.OrdinalIgnoreCase);

        // Assert
        Assert.That(list, Is.EqualTo(new[] { "Apple", "banana", "cherry", "ZEBRA" }));
    }

    /// <summary>
    /// Verifies that Sort does not throw when the list is already sorted.
    /// </summary>
    [Test]
    public void Sort_AlreadySortedList_RemainsUnchanged()
    {
        // Arrange
        var list = new MyList<int> { 1, 2, 3, 4, 5 };

        // Act
        list.Sort();

        // Assert
        Assert.That(list, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
    }

    /// <summary>
    /// Verifies that Sort with null list throws <see cref="ArgumentNullException"/>.
    /// </summary>
    [Test]
    public void Sort_NullList_ThrowsArgumentNullException()
    {
        // Arrange
        MyList<object>? nullList = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => nullList!.Sort());
    }

    /// <summary>
    /// Verifies that Sort works correctly on an empty list.
    /// </summary>
    [Test]
    public void Sort_EmptyList_DoesNothing()
    {
        // Arrange
        var list = new MyList<int>();

        // Act
        list.Sort();

        // Assert
        Assert.That(list.Count, Is.Zero);
    }

    /// <summary>
    /// Verifies that Sort works correctly on a list with a single element.
    /// </summary>
    [Test]
    public void Sort_SingleElementList_RemainsUnchanged()
    {
        // Arrange
        var list = new MyList<double> { 3.14 };

        // Act
        list.Sort();

        // Assert
        Assert.That(list[0], Is.EqualTo(3.14));
    }
}
