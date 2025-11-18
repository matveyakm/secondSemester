// <copyright file="Bwt.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace BWT;

/// <summary>
/// BWT realization.
/// </summary>
public static class Bwt
{
    /// <summary>
    /// word transform.
    /// </summary>
    /// <param name="word">word, which will be transformed.</param>
    /// <returns>transformed word and position.</returns>
    public static (string TransformWord, int Position) Transform(string? word)
    {
        if (word == null)
        {
            return ("Got null as a parameter", -1);
        }

        var shifts = new string[word.Length];
        var currentWord = word;

        for (var i = 0; i < word.Length; i++)
        {
            currentWord = currentWord[1..] + currentWord[0];
            shifts[i] = currentWord;
        }

        Array.Sort(shifts);

        var resultWord = string.Empty;
        foreach (var shift in shifts)
        {
            resultWord += shift[^1];
        }

        int position = Array.IndexOf(shifts, word) + 1;

        return (resultWord, position);
    }

    /// <summary>
    /// reverse word transform.
    /// </summary>
    /// <param name="word">transformed word.</param>
    /// <param name="position">original word index.</param>
    /// <returns>original word.</returns>
    public static string ReverseTransform(string word, int position)
    {
        var len = word.Length;
        var charCounts = new Dictionary<char, int>();
        Dictionary<char, int> startIndexes = new();
        var nextPositions = new int[len];

        var total = 0;
        position--;

        foreach (var chr in word)
        {
            if (!charCounts.TryGetValue(chr, out var value))
            {
                value = 0;
                charCounts[chr] = value;
            }

            charCounts[chr] = ++value;
        }

        char[] sortedLetters = [.. charCounts.Keys];
        Array.Sort(sortedLetters);

        foreach (var letter in sortedLetters)
        {
            startIndexes[letter] = total;
            total += charCounts[letter];
        }

        for (var i = 0; i < len; i++)
        {
            var letter = word[i];
            nextPositions[startIndexes[letter]] = i;
            startIndexes[letter]++;
        }

        var original = new char[len];
        var currentIndex = position;
        for (var i = 0; i < len; i++)
        {
            original[i] = word[nextPositions[currentIndex]];
            currentIndex = nextPositions[currentIndex];
        }

        return new string(original);
    }
}