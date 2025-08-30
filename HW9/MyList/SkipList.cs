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

    /// <inheritdoc/>
    public int Count { get; private set; }

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public T this[int position]
    {
        get
        {
            if (position < 0 || position >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            var currentNode = this.baseNode.Next ?? throw new EmptyCollectionException("Cannot access elements of an empty collection");

            for (var i = 0; i < position; ++i)
            {
                currentNode = currentNode.Next ?? throw new InvalidOperationException("Specified position is beyond collection boundaries");
            }

            if (currentNode.Data == null)
            {
                throw new InvalidOperationException("Collection contains null value at specified position");
            }

            return currentNode.Data;
        }

        set => throw new NotSupportedException("Index-based assignment is not supported");
    }

    /// <inheritdoc/>
    public bool Remove(T element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        if (!this.Contains(element))
        {
            return false;
        }

        var currentNode = this.topNode;
        var removalPerformed = false;

        while (currentNode != null)
        {
            while (currentNode.Next != null
                   && currentNode.Next != this.terminalNode
                   && currentNode.Next.Data != null
                   && currentNode.Next.Data.CompareTo(element) < 0)
            {
                currentNode = currentNode.Next;
            }

            if (currentNode.Next != this.terminalNode &&
                currentNode.Next != null &&
                currentNode.Next.Data != null &&
                currentNode.Next.Data.CompareTo(element) == 0)
            {
                currentNode.Next = currentNode.Next.Next;
                removalPerformed = true;
            }

            currentNode = currentNode.Down;
        }

        if (removalPerformed)
        {
            --this.Count;
            ++this.modificationCounter;
        }

        return removalPerformed;
    }

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

    /// <inheritdoc/>
    public void Clear()
    {
        this.baseNode = new SkipListNode(default, this.terminalNode, this.terminalNode);
        var temp = this.baseNode;

        for (var level = 1; level < MaximumLevel; level++)
        {
            temp = new SkipListNode(default, this.terminalNode, temp);
        }

        this.topNode = temp;
        this.Count = 0;
        ++this.modificationCounter;
    }

    public bool Contains(T element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        var currentNode = this.topNode;

        while (currentNode != null)
        {
            while (currentNode.Next != null && currentNode.Next != this.terminalNode &&
                   currentNode.Next.Data != null &&
                   currentNode.Next.Data.CompareTo(element) < 0)
            {
                currentNode = currentNode.Next;
            }

            if (currentNode.Next != null && currentNode.Next.Data != null &&
                currentNode.Next.Data.CompareTo(element) == 0)
            {
                return true;
            }

            currentNode = currentNode.Down;
        }

        return false;
    }

    /// <inheritdoc/>
    public void CopyTo(T[] targetArray, int startIndex)
    {
        if (targetArray == null)
        {
            throw new ArgumentNullException(nameof(targetArray));
        }

        if (startIndex < 0 || startIndex >= targetArray.Length || startIndex + this.Count > targetArray.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        }

        var currentNode = this.baseNode.Next;

        while (currentNode != this.terminalNode)
        {
            if (currentNode == null || currentNode.Data == null)
            {
                throw new ArgumentException("Invalid element encountered during copy operation");
            }

            targetArray[startIndex] = currentNode.Data;

            ++startIndex;
            currentNode = currentNode.Next;
        }
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.baseNode.Next;
        var initialModificationCount = this.modificationCounter;

        while (currentNode != this.terminalNode && currentNode != null)
        {
            if (initialModificationCount != this.modificationCounter)
            {
                throw new InvalidOperationException("Collection was modified during enumeration");
            }

            if (currentNode.Data == null)
            {
                throw new NullReferenceException("Encountered null element during enumeration");
            }

            yield return currentNode.Data;
            currentNode = currentNode.Next;
        }
    }

    /// <inheritdoc/>
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
}