// <copyright file="LinkedBag.cs" company="matveyakm">
//     Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LinkedBag;

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Bag implementation backed by a singly-linked list.
/// Elements are returned in the order they were added (LIFO-like).
/// </summary>
/// <typeparam name="T">Type of elements in the bag.</typeparam>
public sealed class LinkedBag<T> : ICollection<T>
{
    private Node? head;
    private int count;

    /// <summary>
    /// Initializes a new instance of the <see cref="LinkedBag{T}"/> class.
    /// </summary>
    public LinkedBag()
    {
        this.head = null;
        this.count = 0;
    }

    /// <inheritdoc />
    public int Count => this.count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public void Add(T item)
    {
        this.head = new Node(item, this.head);
        this.count++;
    }

    /// <inheritdoc />
    public void Clear()
    {
        this.head = null;
        this.count = 0;
    }

    /// <inheritdoc />
    public bool Contains(T item)
    {
        var current = this.head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    /// <inheritdoc />
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (arrayIndex + this.count > array.Length)
        {
            throw new ArgumentException("Destination array is not long enough.");
        }

        var current = this.head;
        var index = arrayIndex;
        while (current != null)
        {
            array[index++] = current.Value;
            current = current.Next;
        }
    }

    /// <inheritdoc />
    public bool Remove(T item)
    {
        if (this.head is null)
        {
            return false;
        }

        if (EqualityComparer<T>.Default.Equals(this.head.Value, item))
        {
            this.head = this.head.Next;
            this.count--;
            return true;
        }

        var current = this.head;
        while (current.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Next.Value, item))
            {
                current.Next = current.Next.Next;
                this.count--;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        var current = this.head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private sealed class Node(T value, Node? next = null)
    {
        public T Value { get; set; } = value;

        public Node? Next { get; set; } = next;
    }
}