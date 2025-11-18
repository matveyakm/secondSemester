// <copyright file="MyList.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace List;

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A generic dynamic list implementation that supports adding elements,
/// retrieving the count, and accessing elements by index.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class MyList<T> : IEnumerable<T>
{
    private const int DefaultCapacity = 4;

    private T[] items;
    private int count;

    /// <summary>
    /// Initializes a new instance of the <see cref="MyList{T}"/> class that is empty
    /// and has the default initial capacity.
    /// </summary>
    public MyList()
    {
        this.items = Array.Empty<T>();
        this.count = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyList{T}"/> class that is empty
    /// and has the specified initial capacity.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="capacity"/> is less than 0.
    /// </exception>
    public MyList(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative.");
        }

        this.items = capacity == 0 ? Array.Empty<T>() : new T[capacity];
        this.count = 0;
    }

    /// <summary>
    /// Gets the number of elements contained in the list.
    /// </summary>
    public int Count => this.count;

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.
    /// </exception>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            return this.items[index];
        }

        set
        {
            if (index < 0 || index >= this.count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            this.items[index] = value;
        }
    }

    /// <summary>
    /// Adds an item to the end of the list.
    /// </summary>
    /// <param name="item">The object to be added to the end of the list.</param>
    public void Add(T item)
    {
        if (this.count == this.items.Length)
        {
            this.EnsureCapacity(this.count + 1);
        }

        this.items[this.count] = item;
        this.count++;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the list.
    /// </summary>
    /// <returns>An enumerator for the list.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.count; i++)
        {
            yield return this.items[i];
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private void EnsureCapacity(int minCapacity)
    {
        if (this.items.Length >= minCapacity)
        {
            return;
        }

        int newCapacity = this.items.Length == 0 ? DefaultCapacity : this.items.Length * 2;
        if (newCapacity < minCapacity)
        {
            newCapacity = minCapacity;
        }

        Array.Resize(ref this.items, newCapacity);
    }
}