// <copyright file="Methods.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace MapFilterFold;

/// <summary>
/// realization of Map, Fold, Filter.
/// </summary>
public class Methods
{
    /// <summary>
    /// to apply function to each element of list.
    /// </summary>
    /// <param name="input">input list.</param>
    /// <param name="function">function to apply.</param>
    /// <returns>list after applying function.</returns>
    public static List<int> Map(List<int> input, Func<int, int> function)
    {
        List<int> result = [];

        foreach (var item in input)
        {
            result.Add(function(item));
        }

        return result;
    }

    /// <summary>
    /// to filter list using function.
    /// </summary>
    /// <param name="input">input list.</param>
    /// <param name="filter">filter.</param>
    /// <returns>list with filter.</returns>
    public static List<int> Filter(List<int> input, Func<int, bool> filter)
    {
        var result = new List<int>();

        foreach (var element in input)
        {
            if (filter(element))
            {
                result.Add(element);
            }
        }

        return result;
    }

    /// <summary>
    /// to evaluate the value after going through the list with applying function.
    /// </summary>
    /// <param name="input">list.</param>
    /// <param name="initialValue">initial value.</param>
    /// <param name="function">function to apply.</param>
    /// <returns>result.</returns>
    public static int Fold(List<int> input, int initialValue, Func<int, int, int> function)
    {
        var result = initialValue;

        foreach (var item in input)
        {
            result = function(result, item);
        }

        return result;
    }
}