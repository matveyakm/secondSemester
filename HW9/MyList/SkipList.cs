// <copyright file="SkipList.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace MyList;

using System.Collections;

/// <summary>
/// Represents a collection Skip List.
/// </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
public class SkipList<T> : IList<T>
    where T : IComparable<T>
{
    private const double DefaultThreshold = 0.55;
    private const int MaximumLevel = 32;
    private readonly Random levelRandomizer = new();
    private readonly SkipListNode terminalNode = new(default, null, null);
    private SkipListNode topNode;
    private SkipListNode baseNode;
    private int modificationCounter;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class.
    /// </summary>
    public SkipList()
    {
        this.baseNode = new SkipListNode(default, this.terminalNode, this.terminalNode);
        var temp = this.baseNode;

        for (var level = 0; level < MaximumLevel; level++)
        {
            temp = new SkipListNode(default, this.terminalNode, temp);
        }

        this.topNode = temp;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class with specified elements.
    /// </summary>
    /// <param name="elements">The elements to add to the collection.</param>
    public SkipList(IEnumerable<T> elements)
        : this()
    {
        foreach (var element in elements)
        {
            this.Add(element);
        }
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public int Count { get; private set; }

    public bool IsReadOnly { get; }

    /// <inheritdoc/>
    public void Add(T element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        var newNodeLevel = this.GenerateRandomLevel();
        var currentNode = this.topNode;
        var updatePoints = new SkipListNode[newNodeLevel];

        for (var level = 0; level <= MaximumLevel - newNodeLevel; level++)
        {
            currentNode = currentNode.Down ?? currentNode;
        }

        for (var level = newNodeLevel - 1; level >= 0; --level)
        {
            while (currentNode != null && currentNode.Next != null && currentNode.Next != this.terminalNode && currentNode.Next.Data != null &&
                   currentNode.Next.Data.CompareTo(element) < 0)
            {
                currentNode = currentNode.Next;
            }

            updatePoints[level] = currentNode ?? throw new InvalidOperationException("Invalid node encountered during addition");
            currentNode = currentNode.Down;
        }

        SkipListNode? lowerLevelNode = null;

        for (var level = 0; level < newNodeLevel; ++level)
        {
            var newNode = new SkipListNode(element, updatePoints[level].Next, lowerLevelNode);
            updatePoints[level].Next = newNode;
            lowerLevelNode = newNode;
        }

        ++this.Count;
        ++this.modificationCounter;
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    private int GenerateRandomLevel()
    {
        var level = 1;

        while (this.levelRandomizer.NextDouble() < DefaultThreshold && level < MaximumLevel)
        {
            ++level;
        }

        return level;
    }

    private class SkipListNode(T? data, SkipListNode? next, SkipListNode? down)
    {
        public T? Data { get; set; } = data;

        public SkipListNode? Next { get; set; } = next;

        public SkipListNode? Down { get; set; } = down;
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, T item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    public T this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
}