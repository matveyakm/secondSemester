// <copyright file="LinkedBagTests.cs" company="matveyakm">
//     Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LinkedBag.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

/// <summary>
/// Tests for the LinkedBag class.
/// </summary>
[TestFixture]
public class LinkedBagTests
{
    /// <summary>
    /// Tests that adding an element increases the count of the bag.
    /// </summary>
    [Test]
    public void Add_IncreasesCount()
    {
        var bag = new LinkedBag<int>();
        bag.Add(42);
        Assert.That(bag.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Tests that adding multiple elements to the bag results in the correct count.
    /// </summary>
    /// <remarks>
    /// This test adds three string elements: "a", "b", and "c".
    /// </remarks>
    [Test]
    public void Add_MultipleElements_CountCorrect()
    {
        var bag = new LinkedBag<string>();
        bag.Add("a");
        bag.Add("b");
        bag.Add("c");
        Assert.That(bag.Count, Is.EqualTo(3));
    }

    /// <summary>
    /// Tests that adding duplicate elements is allowed in the bag.
    /// </summary>
    [Test]
    public void Add_Duplicates_Allowed()
    {
        var bag = new LinkedBag<int>();
        bag.Add(1);
        bag.Add(1);
        bag.Add(1);
        Assert.That(bag.Count, Is.EqualTo(3));
    }

    /// <summary>
    /// Tests that the bag can find an existing element.
    /// </summary>
    /// <remarks>
    /// This test adds an element to the bag and checks if it can be found.
    /// </remarks>
    [Test]
    public void Contains_FindsExistingElement()
    {
        var bag = new LinkedBag<string>();
        bag.Add("hello");
        Assert.That(bag.Contains("hello"), Is.True);
    }

    /// <summary>
    /// Tests that the bag does not find an element that was not added.
    /// </summary>
    [Test]
    public void Contains_DoesNotFindMissingElement()
    {
        var bag = new LinkedBag<int>();
        bag.Add(10);
        Assert.That(bag.Contains(20), Is.False);
    }

    /// <summary>
    /// Tests that the bag can find an element that has been added multiple times.
    /// </summary>
    [Test]
    public void Contains_WorksWithDuplicates()
    {
        var bag = new LinkedBag<string>();
        bag.Add("x");
        bag.Add("x");
        Assert.That(bag.Contains("x"), Is.True);
    }

    /// <summary>
    /// Tests that removing one occurrence of an element decreases the count of the bag.
    /// </summary>
    /// <remarks>
    /// This test adds two occurrences of the element and removes one, verifying the count and presence of the element.
    /// </remarks>
    [Test]
    public void Remove_RemovesOneOccurrence()
    {
        var bag = new LinkedBag<int>();
        bag.Add(1);
        bag.Add(2);
        bag.Add(1);
        var removed = bag.Remove(1);
        Assert.That(removed, Is.True);
        Assert.That(bag.Count, Is.EqualTo(2));
        Assert.That(bag.Contains(1), Is.True); // остался ещё один
    }

    /// <summary>
    /// Tests that all occurrences of an element can be removed one by one from the bag.
    /// </summary>
    [Test]
    public void Remove_AllOccurrences_OneByOne()
    {
        var bag = new LinkedBag<string>();
        bag.Add("a");
        bag.Add("a");
        bag.Add("a");

        bag.Remove("a");
        Assert.That(bag.Count, Is.EqualTo(2));

        bag.Remove("a");
        Assert.That(bag.Count, Is.EqualTo(1));

        bag.Remove("a");
        Assert.That(bag.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that removing an element that does not exist in the bag returns false.
    /// </summary>
    /// <remarks>
    /// This test verifies that attempting to remove an element that was not added to the bag
    /// results in a return value of false, and the count remains unchanged.
    /// </remarks>
    [Test]
    public void Remove_MissingElement_ReturnsFalse()
    {
        var bag = new LinkedBag<int>();
        bag.Add(5);
        Assert.That(bag.Remove(999), Is.False);
        Assert.That(bag.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Tests that calling Clear removes all elements from the bag.
    /// </summary>
    [Test]
    public void Clear_RemovesAllElements()
    {
        var bag = new LinkedBag<double>();
        bag.Add(1.1);
        bag.Add(2.2);
        bag.Add(3.3);
        bag.Clear();
        Assert.That(bag.Count, Is.EqualTo(0));
        Assert.That(bag.Contains(1.1), Is.False);
    }

    /// <summary>
    /// Tests that enumeration returns elements in addition order (Last In, First Out).
    /// </summary>
    [Test]
    public void Enumeration_ReturnsElementsInAdditionOrder_LIFO()
    {
        var bag = new LinkedBag<string>();
        bag.Add("first");
        bag.Add("second");
        bag.Add("third");

        var result = bag.ToList();
        Assert.That(result, Is.EqualTo(new[] { "third", "second", "first" }));
    }

    /// <summary>
    /// Tests that enumerating an empty bag returns no elements.
    /// </summary>
    [Test]
    public void Enumeration_EmptyBag_ReturnsEmpty()
    {
        var bag = new LinkedBag<int>();
        Assert.That(bag, Is.Empty);
    }

    /// <summary>
    /// Tests that CopyTo method copies all elements from the bag to an array.
    /// </summary>
    [Test]
    public void CopyTo_CopiesAllElements()
    {
        var bag = new LinkedBag<int>();
        bag.Add(10);
        bag.Add(20);
        bag.Add(30);

        var array = new int[3];
        bag.CopyTo(array, 0);

        Assert.That(array, Is.EqualTo(new[] { 30, 20, 10 }));
    }

    /// <summary>
    /// Tests that the CopyTo method works correctly with an offset.
    /// </summary>
    /// <remarks>
    /// This test verifies that elements are copied to the specified offset in the array.
    /// </remarks>
    [Test]
    public void CopyTo_WithOffset()
    {
        var bag = new LinkedBag<string>();
        bag.Add("a");
        bag.Add("b");

        var array = new string[4];
        bag.CopyTo(array, 1);

        Assert.That(array, Is.EqualTo(new string?[] { null, "b", "a", null }));
    }

    /// <summary>
    /// Tests that the CopyTo method throws an ArgumentNullException when the destination array is null.
    /// </summary>
    [Test]
    public void CopyTo_Throws_ArgumentNullException()
    {
        var bag = new LinkedBag<int>();
        Assert.Throws<ArgumentNullException>(() => bag.CopyTo(null!, 0));
    }

    /// <summary>
    /// Tests that the CopyTo method throws an ArgumentOutOfRangeException when the index is less than zero.
    /// </summary>
    /// <remarks>
    /// This test verifies that attempting to copy elements to a negative index in the destination array
    /// results in an ArgumentOutOfRangeException being thrown.
    /// </remarks>
    [Test]
    public void CopyTo_Throws_ArgumentOutOfRangeException()
    {
        var bag = new LinkedBag<int>();
        bag.Add(1);
        var array = new int[1];
        Assert.Throws<ArgumentOutOfRangeException>(() => bag.CopyTo(array, -1));
    }

    /// <summary>
    /// Tests that the CopyTo method throws an ArgumentException when there is not enough space in the destination array.
    /// </summary>
    /// <remarks>
    /// This test verifies that attempting to copy elements to an array that does not have enough space
    /// results in an ArgumentException being thrown.
    /// </remarks>
    [Test]
    public void CopyTo_Throws_ArgumentException_WhenNotEnoughSpace()
    {
        var bag = new LinkedBag<int>();
        bag.Add(1);
        bag.Add(2);
        var array = new int[1];
        Assert.Throws<ArgumentException>(() => bag.CopyTo(array, 0));
    }

    /// <summary>
    /// Tests that the IsReadOnly property of the bag returns false.
    /// </summary>
    [Test]
    public void IsReadOnly_IsFalse()
    {
        var bag = new LinkedBag<object>();
        Assert.That(bag.IsReadOnly, Is.False);
    }

    /// <summary>
    /// Tests that the bag can handle null values correctly.
    /// </summary>
    [Test]
    public void Works_WithNullValues()
    {
        var bag = new LinkedBag<string?>();
        bag.Add("hello");
        bag.Add(null);
        bag.Add("world");
        bag.Add(null);

        Assert.That(bag.Count, Is.EqualTo(4));
        Assert.That(bag.Contains(null), Is.True);
        Assert.That(bag.Remove(null), Is.True);
        Assert.That(bag.Count, Is.EqualTo(3));
        Assert.That(bag.Contains(null), Is.True); // остался ещё один
    }

    /// <summary>
    /// Tests that the bag can handle custom classes using the Equals method.
    /// </summary>
    /// <remarks>
    /// This test verifies that two instances of the same custom class are considered equal
    /// based on their properties.
    /// </remarks>
    [Test]
    public void Works_WithCustomClass_UsingEquals()
    {
        var bag = new LinkedBag<Person>();
        var p1 = new Person { Name = "Alice", Age = 25 };
        var p2 = new Person { Name = "Alice", Age = 30 };

        bag.Add(p1);
        Assert.That(bag.Contains(p2), Is.True); // Equals по Name
        Assert.That(bag.Remove(p2), Is.True);
        Assert.That(bag.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that the enumerator works correctly for the LinkedBag class.
    /// </summary>
    [Test]
    public void GetEnumerator_IEnumerable_Works()
    {
        var bag = new LinkedBag<int>();
        bag.Add(1);
        bag.Add(2);

        IEnumerable enumerable = bag;
        var result = new List<int>();
        foreach (int x in enumerable)
        {
            result.Add(x);
        }

        Assert.That(result, Is.EqualTo(new[] { 2, 1 }));
    }

    private sealed class Person : IEquatable<Person>
    {
        public string? Name { get; set; }

        public int Age { get; set; }

        public bool Equals(Person? other)
            => other is not null && this.Name == other.Name;

        public override bool Equals(object? obj) => this.Equals(obj as Person);

        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;
    }
}