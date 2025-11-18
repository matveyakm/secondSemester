// <copyright file="MyListTests.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace MyList.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using List;
using NUnit.Framework;

/// <summary>
/// Contains unit tests for the <see cref="MyList{T}"/> class.
/// </summary>
[TestFixture]
public class MyListTests
{
    /// <summary>
    /// Verifies that a newly created list has zero count.
    /// </summary>
    [Test]
    public void Count_WhenNewListIsCreated_ReturnsZero()
    {
        var list = new MyList<int>();

        Assert.That(list.Count, Is.Zero);
    }

    /// <summary>
    /// Verifies that adding an element increases the count by one.
    /// </summary>
    [Test]
    public void Add_SingleElement_CountIsOne()
    {
        var list = new MyList<string>();

        list.Add("test");

        Assert.That(list.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Verifies that added elements can be accessed via indexer.
    /// </summary>
    [Test]
    public void Indexer_AfterAddingElements_ReturnsCorrectValues()
    {
        var list = new MyList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);

        Assert.Multiple(() =>
        {
            Assert.That(list[0], Is.EqualTo(10));
            Assert.That(list[1], Is.EqualTo(20));
            Assert.That(list[2], Is.EqualTo(30));
        });
    }

    /// <summary>
    /// Verifies that setting a value via indexer updates the element.
    /// </summary>
    [Test]
    public void Indexer_SetValue_UpdatesElementCorrectly()
    {
        var list = new MyList<string>();
        list.Add("old");

        list[0] = "new";

        Assert.That(list[0], Is.EqualTo("new"));
    }

    /// <summary>
    /// Verifies that accessing an index out of range throws <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    /// <param name="invalidIndex">
    /// The invalid index to test. Uses values -1 (negative) and 5 (greater than or equal to Count).
    /// </param>
    [Test]
    public void Indexer_InvalidIndex_ThrowsArgumentOutOfRangeException([Values(-1, 5)] int invalidIndex)
    {
        var list = new MyList<int>();
        list.Add(1);
        list.Add(2);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var x = list[invalidIndex];
        });
    }

    /// <summary>
    /// Verifies that the list automatically resizes when capacity is exceeded.
    /// </summary>
    [Test]
    public void Add_ManyElements_ListResizesAutomatically()
    {
        var list = new MyList<int>();

        for (int i = 0; i < 1000; i++)
        {
            list.Add(i);
        }

        Assert.That(list.Count, Is.EqualTo(1000));
        Assert.That(list[999], Is.EqualTo(999));
    }

    /// <summary>
    /// Verifies that the list supports enumeration in correct order.
    /// </summary>
    [Test]
    public void GetEnumerator_ReturnsElementsInInsertionOrder()
    {
        var list = new MyList<char> { 'A', 'B', 'C' };

        var result = new List<char>();
        foreach (var item in list)
        {
            result.Add(item);
        }

        Assert.That(result, Is.EqualTo(new[] { 'A', 'B', 'C' }));
    }
}
