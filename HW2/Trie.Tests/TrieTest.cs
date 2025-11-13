// <copyright file="TrieTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Trie.Tests;

/// <summary>
/// tests for trie.
/// </summary>
public class TrieTest
{
    private Trie trie;

    /// <summary>
    /// to initialize field trie for each test.
    /// </summary>
    [SetUp]
    public void Initialize()
    {
        this.trie = new Trie();
    }

    /// <summary>
    /// test method Add by adding new word.
    /// </summary>
    [Test]
    public void Trie_AddNewWords_ShouldReturnTrue()
    {
        List<string> words = ["mother", "moth", "m"];

        foreach (var word in words)
        {
            Assert.That(this.trie.Add(word), Is.True);
        }
    }

    /// <summary>
    /// test field Size after adding words.
    /// </summary>
    [Test]
    public void Trie_Size_AfterAddingWords()
    {
        List<string> words = ["word", "test", "mum", "word_2"];

        foreach (var word in words)
        {
            this.trie.Add(word);
        }

        Assert.That(this.trie.WordCount, Is.EqualTo(words.Count));
    }

    /// <summary>
    /// test method Add by adding already added word.
    /// </summary>
    [Test]
    public void Trie_Add_AlreadyAddedWord_ShouldReturnFalse()
    {
        const string word = "Word";

        this.trie.Add(word);

        Assert.That(this.trie.Add(word), Is.False);
    }

    /// <summary>
    /// test method Add after deleting word.
    /// </summary>
    [Test]
    public void Trie_Add_AfterDeleting_ShouldReturnTrue()
    {
        const string word = "test";

        this.trie.Add(word);

        this.trie.Remove(word);

        Assert.That(this.trie.Add(word), Is.True);
    }

    /// <summary>
    /// test field Size after deleting word.
    /// </summary>
    [Test]
    public void Trie_Size_AfterDeleting()
    {
        List<string> words = ["word", "test", "Test", "word2"];

        foreach (var word in words)
        {
            this.trie.Add(word);
        }

        this.trie.Remove(words[0]);
        this.trie.Remove(words[1]);

        var expectedResult = words.Count - 2;

        Assert.That(this.trie.WordCount, Is.EqualTo(expectedResult));
    }

    /// <summary>
    /// test field Size of empty trie.
    /// </summary>
    [Test]
    public void Trie_Size_OfEmptyBor()
    {
        const int expectedResult = 0;

        Assert.That(this.trie.WordCount, Is.EqualTo(expectedResult));
    }

    /// <summary>
    /// test method Contains after adding word.
    /// </summary>
    [Test]
    public void Trie_Contains_WordAfterAdding_ShouldReturnTrue()
    {
        const string word = "Word";

        this.trie.Add(word);

        Assert.That(this.trie.Contains(word), Is.True);
    }

    /// <summary>
    /// test method Contains with non-existing word.
    /// </summary>
    [Test]
    public void Trie_Contains_WordWhichNotAdded_ShouldReturnFalse()
    {
        const string word = "test";

        Assert.That(this.trie.Contains(word), Is.False);
    }

    /// <summary>
    /// test method Contains with word after deleting.
    /// </summary>
    [Test]
    public void Trie_Contains_WordAfterDeleting_ShouldReturnFalse()
    {
        const string word = "Word";

        this.trie.Add(word);

        this.trie.Remove(word);

        Assert.That(this.trie.Contains(word), Is.False);
    }

    /// <summary>
    /// test method Remove with word after adding.
    /// </summary>
    [Test]
    public void Trie_Remove_WordAfterAdding()
    {
        const string word = "Word_123";

        this.trie.Add(word);

        this.trie.Remove(word);

        Assert.That(this.trie.Contains(word), Is.False);
    }

    /// <summary>
    /// test method Remove with non-existing word.
    /// </summary>
    [Test]
    public void Trie_Remove_WordWhichWasNotAdded_ShouldReturnFalse()
    {
        const string word = "LLM";

        Assert.That(this.trie.Remove(word), Is.False);
    }
}