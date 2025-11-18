// <copyright file="PriorityQueue.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace PriorityQueue;

/// <summary>
/// Represents a generic priority queue where elements with higher priority are dequeued first.
/// When priorities are equal, elements are dequeued in the order they were enqueued (FIFO - stable sort).
/// </summary>
/// <typeparam name="T">The type of elements in the queue.</typeparam>
public class PriorityQueue<T>
{
    private Node? head;

    /// <summary>
    /// Gets a value indicating whether the queue is empty.
    /// </summary>
    public bool Empty => this.head == null;

    /// <summary>
    /// Adds an element with the specified priority to the queue.
    /// </summary>
    /// <param name="value">The value to enqueue.</param>
    /// <param name="priority">The priority of the value. Higher values mean higher priority.</param>
    public void Enqueue(T value, int priority)
    {
        var newNode = new Node(value, priority);

        if (this.head == null || priority > this.head.Priority)
        {
            newNode.Next = this.head;
            this.head = newNode;
            return;
        }

        var current = this.head;
        while (current.Next != null && current.Next.Priority >= priority)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
    }

    /// <summary>
    /// Removes and returns the element with the highest priority.
    /// </summary>
    /// <returns>The element with the highest priority.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
    public T Dequeue()
    {
        if (this.head == null)
        {
            throw new InvalidOperationException("Cannot dequeue from an empty queue.");
        }

        T value = this.head.Value;
        this.head = this.head.Next;
        return value;
    }

    private sealed class Node
    {
        public Node(T value, int priority)
        {
            this.Value = value;
            this.Priority = priority;
        }

        public T Value { get; }

        public int Priority { get; }

        public Node? Next { get; set; }
    }
}