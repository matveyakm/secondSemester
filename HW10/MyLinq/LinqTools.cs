// <copyright file="LinqTools.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace MyLinq;

/// <summary>
/// methods GetPrimes(), Take(), Skip().
/// </summary>
public static class LinqTools
{
      /// <summary>
    /// Generate infinite sequence of prime numbers.
    /// </summary>
    /// <returns>IEnumerable object of infinite sequence of prime numbers.</returns>
    public static IEnumerable<long> GetPrimes()
    {
        for (long i = 2;; i = checked(i + 1))
        {
            if (IsPrime(i))
            {
                yield return i;
            }
        }
    }

    /// <summary>
    /// return sequence with first n elements.
    /// </summary>
    /// <param name="sequence">input sequence.</param>
    /// <param name=" n">length of sequence.</param>
    /// <typeparam name="T">type of element.</typeparam>
    /// <returns>sequence with first n elements.</returns>
    public static IEnumerable<T> Take<T>(this IEnumerable<T> sequence, int n)
    {
        if (n <= 0)
        {
            yield break;
        }

        var index = 0;

        foreach (var element in sequence)
        {
            yield return element;

            ++index;

            if (index >= n)
            {
                yield break;
            }
        }
    }

    /// <summary>
    /// return sequence with skipped first n elements.
    /// </summary>
    /// <param name="sequence">input sequence.</param>
    /// <param name="n">amount of elements to skip.</param>
    /// <typeparam name="T">type of  element.</typeparam>
    /// <returns>sequence with skipped first n elements.</returns>
    public static IEnumerable<T> Skip<T>(this IEnumerable<T> sequence, int n)
    {
        if (n < 0)
        {
            yield break;
        }

        var index = 0;

        foreach (var element in sequence)
        {
            ++index;

            if (index > n)
            {
                yield return element;
            }
        }
    }

    /// <summary>
    /// check is number prime.
    /// </summary>
    /// <param name="number">number to check.</param>
    /// <returns>false if number isn't prime.</returns>
    private static bool IsPrime(long number)
    {
        if (number <= 1)
        {
            return false;
        }

        for (var i = 2; i * i <= number; ++i)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}