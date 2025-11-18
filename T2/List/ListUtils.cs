// <copyright file="ListUtils.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace List;

using System;
using System.Collections.Generic;

/// <summary>
/// Contains extension methods for <see cref="MyList{T}"/>.
/// </summary>
public static class ListUtils
{
    /// <summary>
    /// Sorts the elements in the entire <see cref="MyList{T}"/> using the specified comparer
    /// <see cref="IComparer{T}"/> with the bubble sort algorithm.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to be sorted.</param>
    /// <param name="comparer">
    /// The <see cref="IComparer{T}"/> implementation to use when comparing elements,
    /// or <c>null</c> to use the default comparer <see cref="Comparer{T}.Default"/>.
    /// </param>
    /// <exception cref="ArgumentNullException"><paramref name="list"/> is <c>null</c>.</exception>
    public static void Sort<T>(this MyList<T> list, IComparer<T>? comparer = null)
    {
        ArgumentNullException.ThrowIfNull(list);

        comparer ??= Comparer<T>.Default;

        int n = list.Count;
        bool swapped;

        do
        {
            swapped = false;
            for (int i = 1; i < n; i++)
            {
                if (comparer.Compare(list[i - 1], list[i]) > 0)
                {
                    (list[i], list[i - 1]) = (list[i - 1], list[i]);
                    swapped = true;
                }
            }

            n--;
        }
        while (swapped);
    }
}