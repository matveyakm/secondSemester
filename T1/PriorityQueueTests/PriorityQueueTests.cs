// <copyright file="PriorityQueueTests.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace PriorityQueue.Tests;

using System;
using System.Collections.Generic;
using NUnit.Framework;

/// <summary>
/// Tests for <see cref="PriorityQueue{T}"/> class.
/// </summary>
[TestFixture]
public class PriorityQueueTests
{
    /// <summary>
    /// Verifies that a single enqueued element is correctly dequeued.
    /// </summary>
    [Test]
    public void Enqueue_SingleElement_DequeueReturnsSameElement()
    {
        var queue = new PriorityQueue<int>();
        queue.Enqueue(42, 10);

        Assert.That(queue.Dequeue(), Is.EqualTo(42));
    }

    /// <summary>
    /// Verifies that elements with different priorities are dequeued in descending priority order.
    /// </summary>
    [Test]
    public void Enqueue_MultipleElementsWithDifferentPriorities_DequeueReturnsInDescendingPriorityOrder()
    {
        var queue = new PriorityQueue<int>();
        queue.Enqueue(1, 10);
        queue.Enqueue(2, 30);
        queue.Enqueue(3, 20);

        var result = new List<int>
        {
            queue.Dequeue(),
            queue.Dequeue(),
            queue.Dequeue(),
        };

        Assert.That(result, Is.EqualTo(new[] { 2, 3, 1 }));
    }

    /// <summary>
    /// Verifies FIFO order when elements have the same priority.
    /// </summary>
    [Test]
    public void Enqueue_ElementsWithSamePriority_DequeueReturnsInEnqueueOrder_FIFO()
    {
        var queue = new PriorityQueue<int>();
        queue.Enqueue(10, 5);
        queue.Enqueue(20, 5);
        queue.Enqueue(30, 5);

        Assert.That(queue.Dequeue(), Is.EqualTo(10));
        Assert.That(queue.Dequeue(), Is.EqualTo(20));
        Assert.That(queue.Dequeue(), Is.EqualTo(30));
    }

    /// <summary>
    /// Verifies that stability is preserved when priorities are mixed.
    /// </summary>
    [Test]
    public void Enqueue_MixedPriorities_StabilityPreservedForEqualPriorities()
    {
        var queue = new PriorityQueue<int>();
        queue.Enqueue(1, 1);
        queue.Enqueue(2, 3);
        queue.Enqueue(3, 2);
        queue.Enqueue(4, 3);
        queue.Enqueue(5, 1);

        var result = new List<int>();
        while (!queue.Empty)
        {
            result.Add(queue.Dequeue());
        }

        Assert.That(result, Is.EqualTo(new[] { 2, 4, 3, 1, 5 }));
    }

    /// <summary>
    /// Verifies that Empty returns true for a newly created queue.
    /// </summary>
    [Test]
    public void Empty_WhenQueueIsEmpty_ReturnsTrue()
    {
        var queue = new PriorityQueue<int>();
        Assert.That(queue.Empty, Is.True);
    }

    /// <summary>
    /// Verifies that Empty returns false after enqueueing an element.
    /// </summary>
    [Test]
    public void Empty_AfterEnqueue_ReturnsFalse()
    {
        var queue = new PriorityQueue<int>();
        queue.Enqueue(1, 1);

        Assert.That(queue.Empty, Is.False);
    }

    /// <summary>
    /// Verifies that Empty returns true after dequeuing the last element.
    /// </summary>
    [Test]
    public void Empty_AfterDequeueLastElement_ReturnsTrue()
    {
        var queue = new PriorityQueue<int>();
        queue.Enqueue(999, 1);
        queue.Dequeue();

        Assert.That(queue.Empty, Is.True);
    }

    /// <summary>
    /// Verifies that Dequeue throws InvalidOperationException on an empty queue.
    /// </summary>
    [Test]
    public void Dequeue_WhenQueueIsEmpty_ThrowsInvalidOperationException()
    {
        var queue = new PriorityQueue<int>();

        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }

    /// <summary>
    /// Verifies that null values are allowed for reference types and maintain FIFO order.
    /// </summary>
    [Test]
    public void Enqueue_NullValueForReferenceType_AllowsNullAndMaintainsOrder()
    {
        var queue = new PriorityQueue<string?>();
        queue.Enqueue("first", 10);
        queue.Enqueue(null, 10);
        queue.Enqueue("third", 10);

        Assert.That(queue.Dequeue(), Is.EqualTo("first"));
        Assert.That(queue.Dequeue(), Is.Null);
        Assert.That(queue.Dequeue(), Is.EqualTo("third"));
    }
}