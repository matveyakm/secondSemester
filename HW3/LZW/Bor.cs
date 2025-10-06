// <copyright file="Bor.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LZW;

/// <summary>
/// realization of Bor.
/// </summary>
public class Bor
{
    private readonly TrieNode root = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Bor"/> class.
    /// </summary>
    public Bor()
    {
        for (var i = 0; i < 256; ++i)
        {
            this.Add([(byte)i], i);
        }
    }

    /// <summary>
    /// Gets or sets amount of added words in trie.
    /// </summary>
    public int WordCount { get; set; }

    /// <summary>
    /// add new word in trie.
    /// </summary>
    /// <param name="word">word to add.</param>
    /// <param name="code">code of word.</param>
    /// <returns>false if word already added.</returns>
    public bool Add(List<byte> word, int code)
    {
        if (word.Count == 0 || code < 0)
        {
            return false;
        }

        var currentNode = this.root;

        foreach (var symbol in word)
        {
            if (!currentNode.Children.TryGetValue(symbol, out var value))
            {
                value = new TrieNode();
                currentNode.Children[symbol] = value;
            }

            currentNode = value;
        }

        if (currentNode.IsTerminal)
        {
            return false;
        }

        currentNode.IsTerminal = true;
        currentNode.Code = code;
        this.WordCount++;

        return true;
    }

    /// <summary>
    /// Check if trie contains element.
    /// </summary>
    /// <param name="word">element to check.</param>
    /// <returns>-1 if trie doesn't contain element, element's code if element is in trie.</returns>
    public int Contains(List<byte> word)
    {
        const int trieDoesNotContainElement = -1;

        if (word.Count == 0)
        {
            return trieDoesNotContainElement;
        }

        var currentNode = this.root;

        foreach (var symbol in word)
        {
            if (!currentNode.Children.TryGetValue(symbol, out var value))
            {
                return trieDoesNotContainElement;
            }

            currentNode = value;
        }

        return currentNode.IsTerminal ? currentNode.Code : -1;
    }

    private class TrieNode
    {
        public Dictionary<byte, TrieNode> Children { get; set; } = new();

        public bool IsTerminal { get; set; }

        public int Code { get; set; } = -1;
    }
}