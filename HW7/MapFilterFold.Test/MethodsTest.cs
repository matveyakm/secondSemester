// <copyright file="MethodsTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace MapFilterFold.Test;

/// <summary>
/// tests for Map, Filter, Fold.
/// </summary>
public class MethodsTest
{
    /// <summary>
    /// test Map with function which add 9 to each element.
    /// </summary>
    [Test]
    public void Methods_Map_IntList_FunctionWhichAddNineEachElement()
    {
        var data = new List<int> { 100, 57, 63, 1, 4 };
        var expectedResult = new List<int> { 109, 66, 72, 10, 13 };

        Assert.That(Methods.Map(data, x => x + 9), Is.EqualTo(expectedResult));
    }

    /// <summary>
    /// test Filter with function which check is number divisible by 5 .
    /// </summary>
    [Test]
    public void Methods_Filter_IntList_ShouldReturnListWithDivisibleByFiveElements()
    {
        var data = new List<int> { 10, 35, 57, 22, 20, 13, 100 };
        var expectedResult = new List<int> { 10, 35, 20, 100 };

        Assert.That(Methods.Filter(data, x => x % 5 == 0), Is.EqualTo(expectedResult));
    }

    /// <summary>
    /// test fold with function which add value to each element of list.
    /// </summary>
    [Test]
    public void Methods_Fold_IntList_FunctionWhichAddValueToEachElement()
    {
        var data = new List<int> { 10, 22, 13 };
        const int start = 10;
        const int expected = 55;

        Assert.That(Methods.Fold(data, start, (current, element) => current + element), Is.EqualTo(expected));
    }
}