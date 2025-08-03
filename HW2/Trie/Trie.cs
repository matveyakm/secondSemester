// <copyright file="Trie.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Trie;

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
    /// Add new word in trie.
    /// </summary>
    /// <param name="word">word to add.</param>
    /// <returns>false if word already added.</returns>
    public bool Add(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return false;
        }

        var node = this.root;

        foreach (var ch in word)
        {
            if (!node.Children.ContainsKey(ch))
            {
                node.Children[ch] = new TrieNode();
            }

            node = node.Children[ch];
            node.TerminalCount++;
        }

        if (node.IsTerminal)
        {
            return false;
        }

        node.IsTerminal = true;
        this.WordCount++;
        return true;
    }

    /// <summary>
    /// check is trie contains element.
    /// </summary>
    /// <param name="word">element to check.</param>
    /// <returns>true if trie contains element.</returns>
    public bool Contains(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return false;
        }

        var node = this.root;

        foreach (var ch in word)
        {
            if (!node.Children.ContainsKey(ch))
            {
                return false;
            }

            node = node.Children[ch];
        }

        return node.IsTerminal;
    }

    /// <summary>
    /// remove element from trie.
    /// </summary>
    /// <param name="word">element to remove.</param>
    /// <returns>true if element was in trie.</returns>
    public bool Remove(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return false;
        }

        var node = this.root;
        var path = new Stack<(TrieNode, char)>();

        foreach (var ch in word)
        {
            if (!node.Children.ContainsKey(ch))
            {
                return false;
            }

            path.Push((node, ch));
            node = node.Children[ch];
        }

        if (!node.IsTerminal)
        {
            return false;
        }

        node.IsTerminal = false;
        this.WordCount--;

        while (path.Count > 0)
        {
            var (parent, ch) = path.Pop();
            var child = parent.Children[ch];

            child.TerminalCount--;

            if (!child.IsTerminal && child.Children.Count == 0)
            {
                parent.Children.Remove(ch);
            }
        }

        return true;
    }

    /// <summary>
    /// returns how many words in trie stars with prefix.
    /// </summary>
    /// <param name="prefix">prefix to check.</param>
    /// <returns>amount of words start with prefix.</returns>
    public int HowManyStartsWithPrefix(string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
        {
            return this.WordCount;
        }

        var node = this.root;

        foreach (var ch in prefix)
        {
            if (!node.Children.ContainsKey(ch))
            {
                return 0;
            }

            node = node.Children[ch];
        }

        return node.TerminalCount;
    }

    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new();

        public bool IsTerminal { get; set; }

        public int TerminalCount { get; set; }
    }
}