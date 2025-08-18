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
    /// Gets or sets amount of added words in trie.
    /// </summary>
    public int WordCount { get; set; }

    /// <summary>
    /// filling trie of bytes.
    /// </summary>
    /// <returns>trie.</returns>
    public static Bor Init()
    {
        Bor newBor = new();

        for (var i = 0; i < 256; ++i)
        {
            newBor.Add([(byte)i], i);
        }

        return newBor;
    }

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
    /// check is trie contains element.
    /// </summary>
    /// <param name="word">element to check.</param>
    /// <returns>-1 if trie doesn't contains element, element's code if element in trie.</returns>
    public int Contains(List<byte> word)
    {
        const int trieDoesNotContainsElement = -1;

        if (word.Count == 0)
        {
            return trieDoesNotContainsElement;
        }

        var currentNode = this.root;

        foreach (var symbol in word)
        {
            if (!currentNode.Children.TryGetValue(symbol, out var value))
            {
                return trieDoesNotContainsElement;
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